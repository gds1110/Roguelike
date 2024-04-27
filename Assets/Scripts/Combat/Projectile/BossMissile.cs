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


    // Update is called once per frame
    void Update()
    {
        _nav.SetDestination(_target.position);
    }
}
