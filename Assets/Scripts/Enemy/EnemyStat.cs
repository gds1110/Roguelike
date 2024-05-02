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
        type = Define.WorldObject.Monster;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void TakeDamage(Stat attacker, BaseCombat combat)
    {
        base.TakeDamage(attacker, combat);
        if (_enemy)
            _enemy.OnAttacked();

    }

    protected override void OnDead(Stat attacker)
    {
        if (_enemy)
        {
            _enemy.OnDead();
            _enemy = null;
        }
        base.OnDead(attacker);
    }

   
}
