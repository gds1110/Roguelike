using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

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

    private void OnTriggerEnter(Collider other)
    {
        OnHit(other);
    }


}
