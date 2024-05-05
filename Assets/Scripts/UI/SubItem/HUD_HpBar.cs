using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD_HpBar : UI_Base
{
    enum GameObjects
    {
        HPBar
    }
    public Stat _stat;

    void Start()
    {
        Init();
    }

    void Update()
    {
        if (_stat != null)
        {
            float ratio = _stat.Hp / (float)_stat.MaxHp;
            SetHpRatio(ratio);
        }
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        _stat = Managers.Game.GetPlayer().GetComponent<Stat>();
    }
    public void SetHpRatio(float ratio)
    {
        GetObject((int)GameObjects.HPBar).GetComponent<Slider>().value = ratio;
    }
}
