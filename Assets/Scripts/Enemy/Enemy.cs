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

    EnemyStat _stat;

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


    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _boxCollider = GetComponent<BoxCollider>();
        _nav = GetComponent<NavMeshAgent>();
        _anim = GetComponentInChildren<Animator>();
        _meshs = GetComponentsInChildren<MeshRenderer>();

        if (_enemyType != Type.D)
        {
            Invoke("ChaseStart", 1.5f);

        }
    }

    void Start()
    {
        _stat = GetComponent<EnemyStat>();

    }

    void ChaseStart()
    {
        _isChase = true;
        _anim.SetBool("IsWalk",true);
    }

    void Update()
    {
        if (_nav.enabled&&_enemyType != Type.D)
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
                _meleeArea.enabled = true;
                yield return new WaitForSeconds(1f);
                _meleeArea.enabled = false;
                yield return new WaitForSeconds(1f);
                break;
            case Type.B:
                yield return new WaitForSeconds(0.1f);
                _rb.AddForce(transform.forward * 20, ForceMode.Impulse);
                _meleeArea.enabled = true;
                yield return new WaitForSeconds(0.5f);
                _rb.velocity= Vector3.zero;
                _meleeArea.enabled = false;
                yield return new WaitForSeconds(2f);
                break;
            case Type.C:
                yield return new WaitForSeconds(0.5f);
                GameObject InstantBullet = Instantiate(_bullet, transform.position, transform.rotation); ;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Projectile")
        {
            StartCoroutine(OnDamage());
        }
    }

    IEnumerator OnDamage()
    {
        foreach(MeshRenderer mesh in _meshs)
        {
            mesh.material.color = Color.red;
        }
        yield return new WaitForSeconds(0.1f);
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

        //TODO
        //Game Manager Despawn
    }
}
