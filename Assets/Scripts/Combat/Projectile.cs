using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    float _lifeTime=10.0f;
    public Poolable _poolable;
    public OrbitBullet _orbit;
    public bool _isPassive = false;
    public bool _isOrbit = false;
    void Start()
    {
        gameObject.layer = 8;
        _poolable = gameObject.GetComponent<Poolable>();
        _orbit = gameObject.GetComponent<OrbitBullet>();
        //if (_poolable != null)
        //{
        //    if (_isPassive == false)
        //    {
        //        StartCoroutine(BackToPool());
        //    }
        //}
    }
    private void OnEnable()
    {
        if (_poolable != null)
        {
            if (_isPassive == false)
            {
                if(_orbit._isOrbit==true)
                {
                    return;
                }
                StartCoroutine(BackToPool());
            }
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
