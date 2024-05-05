using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_GameStart : UI_Scene
{
    enum TEXT
    {
        ScoreText,
        TimeText
    }
    enum BUTTON
    {
        StartBtn
    }

   public Text _scoreText;
   public Text _timeText;
    public Button _startBtn;

    public override void Init()
    {

        Bind<Text>(typeof(TEXT));
        Bind<Button>(typeof(BUTTON));
        _scoreText = GetText((int)TEXT.ScoreText);
        _timeText = GetText((int)TEXT.TimeText);

        int score = PlayerPrefs.GetInt("MaxScore");
        _scoreText.text = $"{score}";

        float maxPlayTime = PlayerPrefs.GetFloat("PlayTime", 0);
        int hour = (int)(maxPlayTime / 3600);
        int min = (int)((maxPlayTime - hour * 3600) / 60);
        int second = (int)(maxPlayTime % 60);
        _timeText.text =string.Format("{0:00}", hour + ":") + string.Format("{0:00}", min + ":") + string.Format("{0:00}", second);
        _startBtn= GetButton((int)BUTTON.StartBtn);
        BindEvent(_startBtn.gameObject, (PointerEventData data) => { Managers.Scene.LoadScene(Define.Scene.Game); });
        //GetButton((int)BUTTON.StartBtn) //.BindEvent((PointerEventData) => { Debug.Log($"아이템 클릭 ! {_name}"); });
    }
}
