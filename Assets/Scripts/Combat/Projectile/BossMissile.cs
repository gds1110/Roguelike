using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossMissile : EnemyProjectile
{

    public Transform _target;
    NavMeshAgent _nav;

    private void Awake()
    {
        _nav = GetComponent<NavMeshAgent>();
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            Stat targetStat = other.GetComponent<Stat>();
            if (targetStat)
            {
                if (_ownerStat == null)
                {
                    _ownerStat = GetComponentInParent<Stat>();
                }
                BaseCombat baseCombat = new BaseCombat();
                baseCombat.AttackType = BaseCombat.EAttackType.Normal;
                baseCombat.Damage = 15;
                targetStat.TakeDamage(_ownerStat, _combat);

            }
            if (_isMeele != true)
            {
                _ownerStat = null;
                Debug.Log("Enemy Bullet Hit");
                Destroy(gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        _nav.SetDestination(_target.position);
        if (_ownerStat == null)
        {
            Destroy(this);
        }
    }
}
