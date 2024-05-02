using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBoss : Enemy
{


    public GameObject _missile;
    public Transform _missilePortA;
    public Transform _missilePortB;


    public float _lookOffset=2f;
    Vector3 _lookVec;
    Vector3 _tauntVec;
    public bool _isLook;

    public float _bossThinkTime = 0.1f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _boxCollider = GetComponent<BoxCollider>();
        _nav = GetComponent<NavMeshAgent>();
        _anim = GetComponentInChildren<Animator>();
        _meshs = GetComponentsInChildren<MeshRenderer>();

        _nav.isStopped = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Think());

        _stat = GetComponent<EnemyStat>();

        Managers.UI.MakeWorldSpaceUI<UI_HPBar>(this.transform).Init();
    }

    // Update is called once per frame
    void Update()
    {
        if(_isDead)
        {
            StopAllCoroutines();
            return;
        }

        if(_isLook )
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            _lookVec = new Vector3(h, 0, v) * _lookOffset;
            transform.LookAt(_target.position+_lookVec);
        }
        else
        {
            _nav.SetDestination(_tauntVec);
        }
    }

    IEnumerator Think()
    {
        yield return new WaitForSeconds(_bossThinkTime);

        int ranAction = Random.Range(0, 5);

        switch(ranAction)
        {

            case 0: 
            case 1:
                StartCoroutine(MissileShot());
                break;            
            case 2:
            case 3:
                StartCoroutine(RockShot());
                break;
            case 4:
                StartCoroutine(Taunt());
                break;
        

        }

    }

    IEnumerator MissileShot()
    {
        _anim.SetTrigger("doShot");
        yield return new WaitForSeconds(0.2f);
        GameObject instantMissileA = Instantiate(_missile,_missilePortA.position, _missilePortA.rotation); 
        BossMissile bma = instantMissileA.GetComponent<BossMissile>();
        bma.SetCombatOwner(_stat, _combat);
        bma._target = _target;
        Destroy(instantMissileA, 5f);

        yield return new WaitForSeconds(0.3f);
        GameObject instantMissileB = Instantiate(_missile, _missilePortB.position, _missilePortB.rotation);
        BossMissile bmb = instantMissileB.GetComponent<BossMissile>();
        bmb.SetCombatOwner(_stat, _combat);
        bmb._target = _target;
        Destroy(instantMissileB, 5f);
        yield return new WaitForSeconds(2f);


        StartCoroutine(Think());

    }
    IEnumerator RockShot()
    {
        _isLook = false;
        _anim.SetTrigger("doBigShot");
        GameObject rock = Instantiate(_bullet, transform.position, transform.rotation);
        BossRock bossRock = rock.GetComponent<BossRock>();
        bossRock.SetCombatOwner(_stat, _combat);
        yield return new WaitForSeconds(3f);
        Destroy(rock, 5f);

        _isLook = true;
        StartCoroutine(Think());

    }

    IEnumerator Taunt()
    {
        _tauntVec = _target.position + _lookVec;

        _isLook = false;
        _nav.isStopped = false;
        _boxCollider.enabled = false;

        _anim.SetTrigger("doTaunt");
        yield return new WaitForSeconds(1.5f);
        _meleeArea.enabled = true;
        yield return new WaitForSeconds(0.5f);
        _meleeArea.enabled = false;
        yield return new WaitForSeconds(1f);

        _isLook = true;
        _nav.isStopped = true;
        _boxCollider.enabled = true;

        StartCoroutine(Think());

    }
}
