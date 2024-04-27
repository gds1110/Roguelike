using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager 
{
 
    public void PlayEffect(Projectile bullet, Vector3 pos,Vector3 normal, Transform parent =null)
    {
        if(bullet._explosionEffect == null)
        {
            return;
        }

        GameObject effect = Object.Instantiate(bullet._explosionEffect,pos,Quaternion.LookRotation(normal));
        //GameObject effect = Object.Instantiate(bullet._explosionEffect,pos,Quaternion.identity);
        effect.SetActive(true);
        if(parent != null)
        {
            effect.transform.parent = parent;  
        }

        Object.Destroy(effect,5f);
    }
}
