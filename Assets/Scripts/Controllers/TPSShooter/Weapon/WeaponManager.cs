using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class WeaponManager : MonoBehaviour
{
    [Header("Fire Rate")]
    [SerializeField] float _fireRate;
    [SerializeField] bool _semiAuto;
    float _fireRateTimer;

    [Header("Bullet Properties")]
    [SerializeField] GameObject bullet;
    [SerializeField] Transform barrelPos;
    [SerializeField] float bulletVelocity;
    [SerializeField] int bulletPerShot;
    AimStateManager _aim;

    WeaponAmmo _ammo;

    void Start()
    {

        _aim = GetComponentInParent<AimStateManager>();
        _fireRateTimer = _fireRate;
        _ammo = GetComponent<WeaponAmmo>(); 
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ShouldFire()) Fire();
    }


    bool ShouldFire()
    {
        _fireRateTimer += Time.deltaTime;
        if (_fireRateTimer < _fireRate) return false;
        if (_ammo._currentAmmo == 0) return false;
        if (_semiAuto && Input.GetKeyDown(KeyCode.Mouse0)) return true;
        if (!_semiAuto && Input.GetKey(KeyCode.Mouse0)) return true;
        return false;
    }

    void Fire()
    {
        _fireRateTimer = 0;
        barrelPos.LookAt(_aim._aimPos);
        Managers.Sound.Play("ShootAudio",Define.Sound.Effect,1.5f);
        _ammo._currentAmmo--;
        for(int i=0;i<bulletPerShot;i++)
        {
            Poolable currentBullet =  Managers.Pool.Pop(bullet);
            currentBullet.transform.position = barrelPos.position;
            Rigidbody rb = currentBullet.GetComponent<Rigidbody>();
            rb.velocity =Vector3.zero;
            rb.AddForce(barrelPos.forward * bulletVelocity, ForceMode.Impulse);
        }
        //fire
    }
}
