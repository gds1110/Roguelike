using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponOrbit : MonoBehaviour
{
    public int _numberOrbit = 5;
    public int _currentNumberOrbit = 5;
    public CharacterController _characterController;
    public WeaponManager _weaponManager;
    public WeaponAmmo _ammo;
    public float _divideAngle;
    public Queue<GameObject> _viewBullets;
    public Queue<GameObject> _offBullets;
    public List<GameObject> _Bullets;
    public GameObject _currentBullet;
    public float _orbitSpeed;


    void Start()
    {
        _characterController = GetComponentInParent<CharacterController>();
        _ammo = GetComponent<WeaponAmmo>();
        _weaponManager = GetComponent<WeaponManager>();
        _orbitSpeed = 30f;
        if (_ammo)
        {
            
            _viewBullets = new Queue<GameObject>();
            _offBullets = new Queue<GameObject>();

            if(_weaponManager != null )
            {
                _currentBullet = _weaponManager.bullet;
                InitOrbit();
            }
           
        }
        _ammo._reloadAction -= ReloadOrbit;
        _ammo._reloadAction += ReloadOrbit;
    

    }


    public void InitOrbit()
    {
        _numberOrbit = _ammo._clipSize;
        _currentNumberOrbit = _numberOrbit;
        for (int i = 0; i < _numberOrbit; i++)
        {
            GameObject go = Managers.Pool.Pop(_currentBullet, this.transform).gameObject;
            if (go != null)
            {
                OrbitBullet orbit = go.GetComponent<OrbitBullet>();
                if (orbit != null)
                {
                    Rigidbody rb = orbit.GetComponent<Rigidbody>();
                    rb.useGravity = false;
                    //Vector3 orbitPos = GetInitOrbitPos(i);
                    //orbit.SetOrbit(_characterController.transform, _orbitSpeed, orbitPos);
                    Vector3 orbitPos = _characterController.transform.position + GetNorm(i);
                    orbit.SetOrbit(_characterController.transform, _orbitSpeed,orbitPos);
                    _viewBullets.Enqueue(go);
                    _Bullets.Add(go);
                }
            }

        }
    }


    public void ReloadOrbit()
    {
        for (int i = 0; i < _Bullets.Count; i++)
        {
            OrbitBullet orbit = _Bullets[i].GetComponent<OrbitBullet>();
            Vector3 orbitPos = _characterController.transform.position + GetNorm(i);
            orbit.SetOrbit(_characterController.transform, _orbitSpeed, orbitPos);
        }
        while (_offBullets.Count > 0)
        {
            GameObject go =_offBullets.Dequeue();
            go.SetActive(true);
            _viewBullets.Enqueue(go);
        }
        _currentNumberOrbit = _numberOrbit;
 
    }

    public void RefershOrbit()
    {


    }
    public void fireOrbit()
    {
        if(_currentNumberOrbit>0&&_viewBullets.Count>0)
        {
            _currentNumberOrbit--;
            GameObject go = _viewBullets.Dequeue();
            go.SetActive(false);
            OrbitBullet orbit = go.gameObject.GetComponent<OrbitBullet>();
            orbit._isOrbit = false;
            _offBullets.Enqueue(go);
        }
    }

    public Vector3 GetNorm(int num)
    {
        float divideAngle = 360f / _numberOrbit;
        float tempAngle = num * divideAngle;
        float tempRadian = tempAngle * Mathf.Deg2Rad;

        return new Vector3(Mathf.Cos(tempRadian), 1f, Mathf.Sin(tempRadian));
    }


    public Vector3 GetInitOrbitPos(int num)
    {
        float divideAngle = 360f / _numberOrbit;
        float tempAngle = num * divideAngle;
        float tempRadian = tempAngle * Mathf.Deg2Rad;
        float tempX = _characterController.transform.position.x;
        float tempY = _characterController.transform.position.y;

        return new Vector3(tempX + Mathf.Cos(tempRadian) * 1f, 1f, tempY + Mathf.Sin(tempRadian) * 1f);
    }

    public Vector3 GetOrbitPos(int num)
    {
        float divideAngle = 360f / _currentNumberOrbit;
        float tempAngle = num * divideAngle;
        float tempRadian = tempAngle * Mathf.Deg2Rad;
        float tempX = _characterController.transform.position.x;
        float tempY = _characterController.transform.position.y;

        return new Vector3(tempX + Mathf.Cos(tempRadian) * 1f, 1f, tempY + Mathf.Sin(tempRadian) * 1f);
    }


}
