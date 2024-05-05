using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingScene : BaseScene
{
    public override void Clear()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        UI_Base EndUI = Managers.UI.ShowSceneUI<UI_Scene>("UI_Ending");
        Init();
        EndUI.GetComponent<UI_Ending>().Init();
    }

    // Update is called once per frame
    void Update()
    {

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
