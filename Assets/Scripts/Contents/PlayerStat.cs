using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class PlayerStat : Stat
{
    [SerializeField]
    protected int _exp;
    [SerializeField]
    protected int _gold;


    bool _isDamage;

    public int Exp 
    { get { return _exp; } 
        set { 
            _exp = value;

            int level = Level;
            while(true)
            {
                Data.Stat stat;
                if (Managers.Data.StatDict.TryGetValue(level + 1, out stat) == false)
                    break;
                if(_exp<stat.totalExp)
                {
                    break;
                }
                level++;
            }
            if(level!=Level)
            {
                Level = level;
                SetStat(Level);
            }
           } 
    }
    public int Gold { get { return _gold; } set { _gold = value; } }


    private void Start()
    {
        _level = 1;
        SetStat(_level);
        _moveSpeed = 5.0f;
        _defense = 5;
        _exp = 0;
        _gold = 0;

    }

    public void SetStat(int level)
    {
        Dictionary<int, Data.Stat> dict = Managers.Data.StatDict;
        Data.Stat stat = dict[level];
        
        _hp = stat.maxHp;
        _maxHp =stat.maxHp;
        _attack =stat.attack;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="EnemyBullet")
        {
            Debug.Log("attacked");
            if(_isDamage)
               StartCoroutine(OnDamage());
        }
    }

    IEnumerator OnDamage()
    {
        _isDamage = true;

        yield return new WaitForSeconds(1f);

        _isDamage = false;
    }
    void NuckBack()
    {

    }

    IEnumerator Knockback()
    {
        Rigidbody rb = GetComponent<Rigidbody>();  
        rb.AddForce(transform.forward*-25,ForceMode.Impulse);
        yield return new WaitForSeconds(1f);

        rb.velocity = Vector3.zero;
    }

    protected override void OnDead(Stat attacker)
    {
        
        


    }
}
