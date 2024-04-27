using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : Stat
{
    Enemy _enemy;
    // Start is called before the first frame update
    void Start()
    {
        _enemy = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void OnDead(Stat attacker)
    {
        base.OnDead(attacker);
        _enemy.OnDead();
    }

    public override void OnAttacked(Stat attacker)
    {
        base.OnAttacked(attacker);
    }
}
