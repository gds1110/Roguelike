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

    protected override void OnDead(Stat attacker)
    {
        PlayerStat playerStat = attacker as PlayerStat;
        if(playerStat != null)
        {
            playerStat.Exp += _exp;
        }


    }
}
