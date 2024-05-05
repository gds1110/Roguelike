using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Poolable : MonoBehaviour
{
    public bool IsUsing;
    public float _lifeTime;



     public void SetBackTime(float lifeTime)
    {
        _lifeTime = lifeTime;
        StartCoroutine(BackToPool());
    }

    IEnumerator BackToPool()
    {
        if (_lifeTime > 0)
        {
            yield return new WaitForSeconds(_lifeTime);
            Managers.Pool.Push(this);
            gameObject.transform.position = Vector3.zero;
        }
        else
        {
            yield return new WaitForSeconds(5.0f);
            Managers.Pool.Push(this);
            gameObject.transform.position = Vector3.zero;
        }

    }
}
