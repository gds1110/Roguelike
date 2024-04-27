using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class WeaponManager : MonoBehaviour
{

    [Header("Bullet Properties")]
    public GameObject bullet;
    [SerializeField] Transform barrelPos;
    [SerializeField] float bulletVelocity;
    [SerializeField] int bulletPerShot;

    public WeaponBased _currentWeapon;
    public WeaponBased _spareWeapon;
    public FireWeapon _fireWeapon;
    public IceWeapon _iceWeapon;

    AimStateManager _aim;


    ActionStateManager _actionState;
    public Action _fireAction;

    public float _lastClickedTime = 0;
    public int _comboCount = 0;
    public int _maxComboCount = 3;

    public Action<WeaponBased> _AchangeWeapon;
    public Action<WeaponBased> _AshootWeapon;
    public Action _AreloadWeapon;

    void Start()
    {

        _aim = GetComponentInParent<AimStateManager>();
        _actionState = GetComponentInParent<ActionStateManager>();
        _fireWeapon = GetComponentInChildren<FireWeapon>();
        _iceWeapon = GetComponentInChildren<IceWeapon>();
        _currentWeapon = _iceWeapon;
        _spareWeapon = _fireWeapon;
        _comboCount = 0;
        _maxComboCount = 3;

        _currentWeapon.WeaponInit();
      
    }

    // Update is called once per frame
    void Update()
    {
        // if (ShouldFire()) Fire();
        if(_currentWeapon==null)
        {
            return;
        }
        if (ShouldFire(_currentWeapon))
        {
            OrbitFire(_currentWeapon);
            if (_AshootWeapon != null)
            {
                _AshootWeapon.Invoke(_currentWeapon);
            }
        }
    }

    bool ShouldFire(WeaponBased weapon)
    {
        if (_actionState._currentState == _actionState._reloadState) return false;

        return _currentWeapon.ShouldFire(weapon);
    }


    public void SwitchWeapon()
    {
        if (_AchangeWeapon == null)
        {
            return;
        }

        if (_currentWeapon is FireWeapon)
        {
           _AchangeWeapon.Invoke(_iceWeapon);
            _currentWeapon = _iceWeapon;
            _spareWeapon = _fireWeapon;
        }
        else if(_currentWeapon is IceWeapon)
        {
           _AchangeWeapon.Invoke(_fireWeapon);
            _currentWeapon = _fireWeapon;
            _spareWeapon = _iceWeapon;

        }
        _currentWeapon._weaponOrbit.ChangedOrbit(_currentWeapon);
    }
    void Fire()
    {
        _currentWeapon._fireRateTimer = 0;
        barrelPos.LookAt(_aim._aimPos);
        Managers.Sound.Play("ShootAudio",Define.Sound.Effect,1.5f);
        _currentWeapon._currentAmmo--;
        for(int i=0;i<bulletPerShot;i++)
        {
            Poolable currentBullet =  Managers.Pool.Pop(bullet);
            currentBullet.transform.position = barrelPos.position;
            Rigidbody rb = currentBullet.GetComponent<Rigidbody>();
            rb.useGravity = true;
            rb.velocity =Vector3.zero;
            rb.AddForce(barrelPos.forward * bulletVelocity, ForceMode.Impulse);
        }
        //fire
    }

    void OrbitFire(WeaponBased weapon)
    {
        Debug.Log("Orbit Fire");
        barrelPos.LookAt(_aim._aimPos);

        CheckFireTime();
        Managers.Sound.Play("ShootAudio", Define.Sound.Effect, 1.5f);

        _comboCount++;
        _actionState._anim.SetInteger("ComboCount", _comboCount % _maxComboCount);
        _actionState._anim.SetTrigger("Fire");

        for (int i = 0; i < bulletPerShot; i++)
        {
            Poolable currentBullet = Managers.Pool.Pop(_currentWeapon._bullet);
            currentBullet.transform.position = barrelPos.position;
            Rigidbody rb = currentBullet.GetComponent<Rigidbody>();
            rb.useGravity = true;
            rb.velocity = Vector3.zero;
            rb.AddForce(barrelPos.forward * bulletVelocity, ForceMode.Impulse);
            _currentWeapon.OrbitFire(weapon);
        }
    }

    void CheckFireTime()
    {
        if(_lastClickedTime-Time.time>1)
        {
            _comboCount = 0;
        }

        _lastClickedTime = Time.time; 
    }
}
