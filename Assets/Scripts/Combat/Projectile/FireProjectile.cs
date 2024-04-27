using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireProjectile : Projectile
{
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (_orbit._isOrbit == true)
        {
            return;
        }
        if (other.gameObject.layer == 6 || other.gameObject.layer == 9)
        {
            _poolable.gameObject.transform.position = Vector3.zero;
        }
        if (_explosionEffect != null)
        {

            _explosionEffect.SetActive(true);
        }
        Invoke("BackPool", 0.1f);
    }
}
