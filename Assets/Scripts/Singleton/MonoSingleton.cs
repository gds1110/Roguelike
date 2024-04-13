using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T s_instance = null;
    public static T Instance { 
        get
        {
            if (s_instance == null)
            {
                s_instance = new GameObject(typeof(T).ToString(),typeof(T)).GetComponent<T>();
            }

            return s_instance; 
        }
    
    }  

    public virtual void Init() { }

    public void Awake()
    {
        if(s_instance == null) 
        {
            s_instance = this as T;
            Init();
        }
    }
}
