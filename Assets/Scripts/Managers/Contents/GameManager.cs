using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Recorder.OutputPath;

public class GameManager 
{
    GameObject _player;
   // Dictionary<int, GameObject> _monsters = new Dictionary<int, GameObject>();
   public HashSet<GameObject> _monsters= new HashSet<GameObject>();

    public Action<int> OnSpawnEvent;
    public Action GameOverTimeEvent;
    public int _score=0;
    public bool _isBattle = false;
    public float _playTime = 0;
    public void Init()
    {
    }

    public GameObject Spawn(Define.WorldObject type,string path,Transform parent = null)
    {
        GameObject go =  Managers.Resource.Instantiate(path, parent);

        switch (type)
        {
            case Define.WorldObject.Monster:
                _monsters.Add(go);
                if(OnSpawnEvent!=null)
                {
                    OnSpawnEvent.Invoke(1);
                }
                break;
            case Define.WorldObject.Player:
                _player = go;
                break;


        }

        return go;
    }

    public GameObject SpawnEnemy(Transform parent = null,string name = null)
    {
       
        GameObject go = Managers.Resource.Instantiate($"Enemys/{name}");

        go.transform.SetParent(parent);
        _monsters.Add(go);
        if (OnSpawnEvent != null)
            OnSpawnEvent.Invoke(1);

        return go;

    }


    public GameObject GetPlayer()
    {
        return _player;
    }
    public void SetPlayer(GameObject player)
    {
        _player = player;
    }

    public Define.WorldObject GetWorldObjectType(GameObject go)
    {   Stat stat = go.GetComponent<Stat>();
        if(stat==null)
        {
            return Define.WorldObject.Unknown;
        }

        return stat.type;

    }
    public void Despawn(GameObject go)
    {
        Define.WorldObject type = GetWorldObjectType(go);

        switch(type)
        {
            case Define.WorldObject.Monster:
                {
                    if (_monsters.Contains(go))
                        _monsters.Remove(go);
                    if(OnSpawnEvent!=null)
                    { OnSpawnEvent.Invoke(-1); }
                }
                 break;
            case Define.WorldObject.Player:
                if(_player==go)
                {
                    _player = null;
                }
                break;
        }

        Managers.Resource.Destroy(go);
    }

    public void AddScore(int score)
    {
        _score += score;
    }

    public void GameOver()
    {
        _isBattle = false;
        GameOverTimeEvent?.Invoke();
        float maxPlayTime = PlayerPrefs.GetFloat("PlayTime",0);
        if(_playTime > maxPlayTime)
        {
            PlayerPrefs.SetFloat("PlayTime", _playTime);
        }
        int maxScore = PlayerPrefs.GetInt("MaxScore",0);
        if (_score > maxScore)
        {
            PlayerPrefs.SetInt("MaxScore", _score);
        }

        Managers.Scene.LoadScene(Define.Scene.EndingScene);
    }
}
