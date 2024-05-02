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

        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;

        Dictionary<int, Data.Stat>dict = Managers.Data.StatDict;
        //GameObject player = Managers.Game.Spawn(Define.WorldObject.Player, "UnityChan");


        //GameObject go = new GameObject { name = "Genrator" };
        //EnemyGenerator generator = go.GetOrAddComponent<EnemyGenerator>();
        //generator.SetKeepMonsterCount(5);
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
