using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD_Score : UI_Base
{

    enum TEXT
    {
        ScoreText,
    }
    Text _scoreText;
    public override void Init()
    {
        Bind<Text>(typeof(TEXT));
        _scoreText = GetText((int)TEXT.ScoreText);
    }

    private void LateUpdate()
    {
        _scoreText.text = $"{Managers.Game._score}";
    }

}
