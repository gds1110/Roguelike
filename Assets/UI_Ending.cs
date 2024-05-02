using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Ending : UI_Scene
{
    enum TEXT
    {
        ScoreText,
        TimeText
    }
    enum BUTTON
    {
        EndingBtn
    }

    public Text _scoreText;
    public Text _timeText;
    public Button _restartBtn;
    public override void Init()
    {

        Bind<Text>(typeof(TEXT));
        Bind<Button>(typeof(BUTTON));
        _scoreText = GetText((int)TEXT.ScoreText);
        _timeText = GetText((int)TEXT.TimeText);

        int score = Managers.Game._score;
        _scoreText.text = $"{score}";

        float maxPlayTime = Managers.Game._playTime;
        int hour = (int)(maxPlayTime / 3600);
        int min = (int)((maxPlayTime - hour * 3600) / 60);
        int second = (int)(maxPlayTime % 60);
        _timeText.text = string.Format("{0:00}", hour + ":") + string.Format("{0:00}", min + ":") + string.Format("{0:00}", second);
        _restartBtn = GetButton((int)BUTTON.EndingBtn);
        BindEvent(_restartBtn.gameObject, (PointerEventData data) => { Managers.Scene.LoadScene(Define.Scene.Login); });
        //GetButton((int)BUTTON.StartBtn) //.BindEvent((PointerEventData) => { Debug.Log($"아이템 클릭 ! {_name}"); });
    }

}
