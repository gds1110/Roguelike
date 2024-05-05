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
    public BaseCombat _combat;
    public Stat _owner;

    public bool _isRotating = false;
    void Start()
    {
      Init();
    }


    protected virtual void Init()
    {
       
        _poolable = gameObject.GetComponent<Poolable>();
        _orbit = gameObject.GetComponent<OrbitBullet>();

    }
    public void SetCombat(BaseCombat combat)
    {
        _combat =combat;
    }
    public void SetOwner(Stat stat)
    {
        _owner = stat;
    }
    protected virtual void OnHit(Collider other)
    {
        if (_orbit._isOrbit == true)
        {
            return;
        }
        if (other.gameObject.tag == "Monster" || other.gameObject.layer == (1 << 9))
        {

            Vector3 norm = Vector3.Normalize(gameObject.transform.position - other.transform.position);
            Managers.Effect.PlayEffect(this, other.transform.position, norm);


            _poolable.gameObject.transform.position = Vector3.zero;
        }

        Stat targetStat = other.GetComponent<Stat>();
        if (targetStat)
        {

            targetStat.TakeDamage(_owner, _combat);

        }
        Invoke("BackPool", 0.5f);
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
