using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    float _lifeTime=1.0f;
   public Poolable _poolable;
    void Start()
    {
        gameObject.layer = 8;
        _poolable = gameObject.GetComponent<Poolable>();

        if (_poolable != null)
        {
            StartCoroutine(BackToPool());
        }
    }
    private void OnEnable()
    {
        if (_poolable != null)
        {
            StartCoroutine(BackToPool());
        }
    
    }
    IEnumerator BackToPool()
    {   
            if (_lifeTime > 0)
            {
                yield return new WaitForSeconds(_lifeTime);
                _poolable.gameObject.transform.position = Vector3.zero; 
                Managers.Pool.Push(_poolable);
            }
            else
            {
                yield return new WaitForSeconds(5.0f);
            _poolable.gameObject.transform.position = Vector3.zero;
            Managers.Pool.Push(_poolable);
            }
        
    }
}
