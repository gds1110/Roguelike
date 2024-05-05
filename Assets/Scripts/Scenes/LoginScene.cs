using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Login;


    }

    public override void Clear()
    {
    }


    // Start is called before the first frame update
    void Start()
    {


        UI_Base StartUI = Managers.UI.ShowSceneUI<UI_Scene>("UI_Start");
        Init();
        StartUI.GetComponent<UI_GameStart>().Init();
    }
    private void Update()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

}
