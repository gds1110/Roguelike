using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class WeaponBased : MonoBehaviour
{

    [Header("Fire Rate")]
    public float _fireRate;
    public bool _semiAuto;
    public float _fireRateTimer;

    [Header("Bullet Properties")]
    public GameObject _bullet;
    public int _bulletDamage = 10;

    public WeaponAmmo _ammo;
    public WeaponOrbit _weaponOrbit;

    public int _ammoSize;
    public int _currentAmmo;

    public Sprite _weaponImage;
    protected virtual void Start()
    {
        _ammo = Util.GetOrAddComponent<WeaponAmmo>(this.gameObject);
        _weaponOrbit = Util.GetOrAddComponent<WeaponOrbit>(gameObject);
        _fireRateTimer = _fireRate;
        _currentAmmo = _ammoSize;
    }

    public virtual void WeaponInit()
    {
        _ammo._clipSize = _ammoSize;
        _ammo._currentAmmo = _currentAmmo;
        _weaponOrbit.ChangedOrbit(this);

    }

    public virtual bool ShouldFire(WeaponBased weapon)
    {
        _fireRateTimer += Time.deltaTime;
        if (_fireRateTimer < _fireRate) return false;
        if (_ammo._currentAmmo == 0)
        {
            return false;
        }
        if (_semiAuto && Input.GetKeyDown(KeyCode.Mouse0)) return true;
        if (!_semiAuto && Input.GetKey(KeyCode.Mouse0)) return true;
        return false;
    }

    public virtual void OrbitFire(WeaponBased weapon)
    {
        _fireRateTimer = 0;
        Managers.Sound.Play("ShootAudio", Define.Sound.Effect, 1.5f);
        _ammo._currentAmmo--;
        _weaponOrbit.fireOrbit();
    }
}
