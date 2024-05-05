using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Stat : MonoBehaviour
{
    [SerializeField]
   protected int _level;
    [SerializeField]
    protected int _hp;
    [SerializeField]
    protected int _maxHp;
    [SerializeField]
    protected int _attack;
    [SerializeField]
    protected int _defense;
    [SerializeField]
    protected float _moveSpeed;

    public int Level { get { return _level; } set { _level = value; } }
    public int Hp { get { return _hp;} set { _hp = value; } }             
    public int MaxHp { get { return _maxHp;}   set   {    _maxHp = value;        }      }
    public int Attack { get { return _attack;}   set    {   _attack = value;        }       }  
    public int Defense { get { return _defense;}   set    {     _defense = value;        }       }
    public float MoveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }

    public Define.WorldObject type;

    private void Start()
    {
        _level = 1;
        _hp = 100;
        _maxHp = 100;    
        _attack = 10;
        _defense = 5;
        _moveSpeed = 5.0f;      
    }

    protected virtual void OnDead(Stat attacker)
    {
        //PlayerStat playerStat = attacker as PlayerStat;
        //if(playerStat != null)
        //{
        //    playerStat.Exp += 1;
        //}

        //Managers.Pool.Pop();

        Managers.Game.Despawn(gameObject);
    }
    protected virtual void OnDead()
    {
        Managers.Game.Despawn(gameObject);
    }
    public virtual void TakeDamage(Stat attacker, BaseCombat combat)
    {
        int damage = Mathf.Max(0,(attacker._attack+combat.Damage)-_defense);
        _hp -= damage;
        if(_hp <= 0 )
        {
            _hp = 0;
            OnDead(attacker);
        }
        switch (combat.AttackType)
        {
            case BaseCombat.EAttackType.Normal:
                break;
            case BaseCombat.EAttackType.KnockBack:
                StartCoroutine(Knockback());
                break;
            case BaseCombat.EAttackType.Fire:
                break;
            case BaseCombat.EAttackType.Ice:
                break;
            case BaseCombat.EAttackType.Boom:
                break;
        }

    }



    public IEnumerator Knockback()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * -25, ForceMode.Impulse);
        yield return new WaitForSeconds(1f);

        rb.velocity = Vector3.zero;
    }
}


