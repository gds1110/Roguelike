using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class IceProjectile : Projectile
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
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (_orbit._isOrbit == true)
    //    {
    //        return;
    //    }

    //    if (collision.gameObject.layer == 6 || collision.gameObject.layer == 9)
    //    {
    //        _poolable.gameObject.transform.position = Vector3.zero;
    //    }
    //    if (_explosionEffect != null)
    //    {
    //        _explosionEffect.SetActive(true);
    //    }
    //    Invoke("BackPool", 0.1f);

    //}
    private void OnTriggerEnter(Collider other)
    {
        if (_orbit._isOrbit == true)
        {
            return;
        }
        if (other.gameObject.tag == "Monster" || other.gameObject.layer == (1 << 9))
        { 

            Vector3 norm = Vector3.Normalize(gameObject.transform.position-other.transform.position);
            Managers.Effect.PlayEffect(this, other.transform.position, norm);


            _poolable.gameObject.transform.position = Vector3.zero;
        }

        Invoke("BackPool", 0.5f);
    }


}
