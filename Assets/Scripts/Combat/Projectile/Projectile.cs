using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    float _lifeTime=10.0f;
    public Poolable _poolable;
    public OrbitBullet _orbit;
    public bool _isPassive = false;
    public bool _isOrbit = false;
    public GameObject _explosionEffect;

    public bool _isRotating = false;
    void Start()
    {
      Init();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_orbit != null)
        {
            if (_orbit._isOrbit == true)
            {
                return;
            }
        }
        if (other.gameObject.layer == (1<<6) || other.gameObject.layer ==(1<<9))
        {
            Debug.Log("BulletHit");
            _poolable.gameObject.transform.position = Vector3.zero;

            if (_explosionEffect != null)
            {

                _explosionEffect.SetActive(true);
            }
        }
       
        Invoke("BackPool", 0.1f);
    }

    protected virtual void Init()
    {
       
        _poolable = gameObject.GetComponent<Poolable>();
        _orbit = gameObject.GetComponent<OrbitBullet>();
        if (_explosionEffect!=null)
        {
            _explosionEffect.SetActive(false);
        }
    }

    protected void BackPool()
    {
        Managers.Pool.Push(_poolable);
    }
    private void OnDisable()
    {

        if (_explosionEffect != null)
        {
            _explosionEffect.SetActive(false);
        }

    }


}
