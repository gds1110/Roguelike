using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct BaseCombat 
{
    public enum EAttackType
    {
        Normal,
        KnockBack,
        Fire,
        Ice,
        Boom,
    }


    public int Damage
    {
        get
        {
            return _damage;
        }
        set
        {
            _damage = value;
        }
    }
    public EAttackType AttackType
    {
        get { return _attackType; }
        set { _attackType = value; }
    }


    private int _damage;
    private EAttackType _attackType;
}
