using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public bool _isMeele =false;
    public int _damage;
    public BaseCombat _combat;
    public Stat _ownerStat;

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            Stat targetStat = other.GetComponent<Stat>();
            if (targetStat)
            {
                if (_ownerStat == null)
                {
                    _ownerStat = GetComponentInParent<Stat>();
                }
                targetStat.TakeDamage(_ownerStat, _combat);

            }
            if (_isMeele != true)
            {
                _ownerStat = null;
                Debug.Log("Enemy Bullet Hit");
                Destroy(gameObject);
            }
        }
    }

    public virtual void SetCombatOwner(Stat owner, BaseCombat combat)
    {
        _ownerStat = owner;
        _combat = combat;
    }
}
