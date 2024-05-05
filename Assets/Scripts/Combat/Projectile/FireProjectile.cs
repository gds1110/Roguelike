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
        Collider[] hitCols = Physics.OverlapSphere(gameObject.transform.position, 2.0f,1<<6);
        for(int i=0;i<hitCols.Length;i++)
        {
            if (hitCols[i].gameObject==other.gameObject)
            {
                continue;
            }
         
                Vector3 norm = Vector3.Normalize(gameObject.transform.position - hitCols[i].gameObject.transform.position);
                Managers.Effect.PlayEffect(this, hitCols[i].gameObject.transform.position, norm);
          
            Stat targetStat = hitCols[i].gameObject.GetComponent<Stat>();
            if (targetStat)
            {
                BaseCombat tempcombat = _combat;
                tempcombat.Damage=_combat.Damage/2;
                targetStat.TakeDamage(_owner, tempcombat);

            }
        }
        OnHit(other);

    }
}
