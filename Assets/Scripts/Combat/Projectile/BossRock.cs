using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRock : EnemyProjectile
{
    Rigidbody _rb;
    public float _angularPower = 30;
    public float _addAngularPower = 0.05f;
    public float _scaleValue = 0.1f;
    public float _addScaleValue = 0.002f;

    bool _isShoot;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();

        StartCoroutine(GainPowerTimer());
        StartCoroutine(GainPower());
    }
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
                BaseCombat baseCombat = new BaseCombat();
                baseCombat.AttackType = BaseCombat.EAttackType.KnockBack;
                baseCombat.Damage = 30;
                targetStat.TakeDamage(_ownerStat, baseCombat);

            }
            if (_isMeele != true)
            {
                _ownerStat = null;
                Debug.Log("Enemy Bullet Hit");
                Destroy(gameObject);
            }
        }
    }
    IEnumerator GainPowerTimer()
    {

        yield return new WaitForSeconds(2.2f);
        _isShoot = true;

    }
    IEnumerator GainPower()
    {
        while(!_isShoot)
        {
            _angularPower += _addAngularPower;
            _scaleValue += _addScaleValue;
            transform.localScale = Vector3.one* _scaleValue;
            _rb.AddTorque(transform.right * _angularPower, ForceMode.Acceleration);
            yield return null;
        }
    }
}
