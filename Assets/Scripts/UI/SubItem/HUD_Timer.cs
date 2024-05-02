using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HUD_Timer : UI_Base
{
    enum TEXT
    {
        TimerText,
    }

    public override void Init()
    {
        Bind<Text>(typeof(TEXT)); 
    }

    float _playTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        Managers.Game._isBattle = true;

        Managers.Game.GameOverTimeEvent -= SetPlayTime;
        Managers.Game.GameOverTimeEvent += SetPlayTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(Managers.Game._isBattle)
        {
            _playTime += Time.deltaTime;
        }
    }

    private void LateUpdate()
    {
        int hour = (int)(_playTime / 3600);
        int min = (int)((_playTime-hour*3600) / 60);
        int second = (int)(_playTime % 60);

        GetText((int)TEXT.TimerText).text = string.Format("{0:00}", hour + ":") + string.Format("{0:00}", min + ":") + string.Format("{0:00}",second);

    }
    public void SetPlayTime()
    {

        Managers.Game._playTime = _playTime;
    }
}
