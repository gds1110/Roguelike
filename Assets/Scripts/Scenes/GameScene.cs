using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{


    void Start()
    {
        Init(); 
    }
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;


        Dictionary<int, Data.Stat>dict = Managers.Data.StatDict;

    }
    public override void Clear()
    {
        
    }

    IEnumerator ExplodeAfterSeconds(float seconds)
    {
        Debug.Log("Start Explode");
        yield return new WaitForSeconds(seconds);
        Debug.Log("Boom");
    }

}
