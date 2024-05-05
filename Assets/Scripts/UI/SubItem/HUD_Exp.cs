using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD_Exp :  UI_Base
{
    enum TEXT
    {
       ExpText,
    }
    enum Slide
    {
        ExpSlider,
    }
    Slider _expSlider;
    public override void Init()
    {
        Bind<Text>(typeof(TEXT));
        Bind<GameObject>(typeof(Slide));
        _expSlider = GetObject((int)Slide.ExpSlider).GetComponent<Slider>();
        PlayerStat playerStat = Managers.Game.GetPlayer().GetComponent<PlayerStat>();
        _expSlider.value = playerStat.Exp / (float)playerStat.GetTotalExp();
    }

    private void LateUpdate()
    {
        PlayerStat playerStat = Managers.Game.GetPlayer().GetComponent<PlayerStat>();
        _expSlider.value = playerStat.Exp/ (float)playerStat.GetTotalExp();
    }
}
