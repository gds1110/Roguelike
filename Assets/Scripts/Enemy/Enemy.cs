using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform _target;


    public enum Type { A,B,C,D}
    public Type _enemyType;

   public EnemyStat _stat;
   public BaseCombat _combat;

    public Rigidbody _rb;
     public BoxCollider _boxCollider;
    public MeshRenderer[] _meshs;
     public NavMeshAgent _nav;
    
    public Animator _anim;
    public bool _isChase;
    public bool _isAttack;
    public bool _isDead=false;

    public BoxCollider _meleeArea;
    public GameObject _bullet;
    public GameObject _ExpOrb;
    public int _score;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _boxCollider = GetComponent<BoxCollider>();
        _nav = GetComponent<NavMeshAgent>();
        _anim = GetComponentInChildren<Animator>();
        _meshs = GetComponentsInChildren<MeshRenderer>();
        _stat = GetComponent<EnemyStat>();
        _combat = new BaseCombat();
        _combat.Damage = _stat.Attack;

        if (_enemyType != Type.D)
        {
            Invoke("ChaseStart", 1.5f);

        }
    }

    void Start()
    {
        _stat = GetComponent<EnemyStat>();

        Managers.UI.MakeWorldSpaceUI<UI_HPBar>(this.transform).Init();

    }

    void ChaseStart()
    {
        _isChase = true;
        _anim.SetBool("IsWalk",true);
    }

    void Update()
    {
        if (_nav.enabled&&_enemyType != Type.D&&_target!=null)
        {
            _nav.SetDestination(_target.position);
            _nav.isStopped = !_isChase;
        }
    }

    void Targeting()
    {
        if(_enemyType != Type.D)
        {

            float targetRadius = 1.5f;
            float targetRange = 0.8f;

            switch (_enemyType)
            {
                case Type.A:
                    targetRadius = 1.5f;
                    targetRange = 0.8f;
                    break;
                case Type.B:
                    targetRadius = 1f;
                    targetRange = 10f;
                    break;
                case Type.C:
                    targetRadius = 0.5f;
                    targetRange = 20f;
                    break;
            }

            RaycastHit[] rayHits =
                Physics.SphereCastAll(transform.position,
                targetRadius,
                transform.forward,
                targetRange,
                LayerMask.GetMask("Player")
                );

            if (rayHits.Length > 0 && !_isAttack)
            {
                StartCoroutine(Attack());
            }
        }
     
    }

    IEnumerator Attack()
    {
        _isChase = false;
        _isAttack = true;
        _anim.SetBool("IsAttack", true);


        switch (_enemyType)
        {
            case Type.A:
                yield return new WaitForSeconds(0.2f);
                transform.LookAt(_target.position);
                _meleeArea.enabled = true;
                _meleeArea.GetComponent<EnemyProjectile>().SetCombatOwner(_stat, _combat);
                yield return new WaitForSeconds(1f);
                _meleeArea.enabled = false;
                yield return new WaitForSeconds(1f);
                break;
            case Type.B:
                yield return new WaitForSeconds(0.1f);
                transform.LookAt(_target.position);
                _rb.AddForce(transform.forward * 20, ForceMode.Impulse);
                _meleeArea.enabled = true;
                _meleeArea.GetComponent<EnemyProjectile>().SetCombatOwner(_stat, _combat);
                yield return new WaitForSeconds(0.5f);
                _rb.velocity= Vector3.zero;
                _meleeArea.enabled = false;
                yield return new WaitForSeconds(2f);
                break;
            case Type.C:
                yield return new WaitForSeconds(0.5f);
                transform.LookAt(_target.position);
                GameObject InstantBullet = Instantiate(_bullet, transform.position, transform.rotation);
                InstantBullet.GetComponent<EnemyProjectile>().SetCombatOwner(_stat, _combat);
                Rigidbody rbBullet = InstantBullet.GetComponent<Rigidbody>();
                rbBullet.velocity = transform.forward * 20;
                yield return new WaitForSeconds(2f);
                break;
        }


 
        _isChase = true;
        _isAttack = false;
        _anim.SetBool("IsAttack", false);

    }

    private void FixedUpdate()
    {
        if (_isDead == false)
        {
            Targeting();
        }
          FreezeVelocity();
    }

    public void OnAttacked()
    {
        StartCoroutine(OnDamage());
    }

    public IEnumerator OnDamage()
    {
        foreach(MeshRenderer mesh in _meshs)
        {
            mesh.material.color = Color.red;
        }
        yield return new WaitForSeconds(0.3f);
        foreach (MeshRenderer mesh in _meshs)
        {
            mesh.material.color = Color.white;
        }
    }
    void FreezeVelocity()
    {
        if(_isChase)
        {
            _rb.velocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;
        }
    }
    public void OnDead()
    {

        _nav.enabled = false;
        _isChase = false;
        _anim.SetTrigger("doDie");
        Managers.Pool.Pop(_ExpOrb).transform.position = this.gameObject.transform.position+new Vector3(0,1,0);
        Managers.Game.AddScore(_ExpOrb.GetComponent<Exporb>().exp);
        //TODO
        //Game Manager Despawn in stat

    }
}
