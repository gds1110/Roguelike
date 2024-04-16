using System.Collections;
using System.Collections.Generic;
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

            if(_weaponManager != null )
            {
                _currentBullet = _weaponManager.bullet;

                ReloadOrbit();
            }
           
        }
        _ammo._reloadAction -= ReloadOrbit;
        _ammo._reloadAction += ReloadOrbit;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GetDivideAngle()
    {
        float divideAngle = 360f / _numberOrbit;
        for(int i=0;i<_numberOrbit;i++)
        {

        }
    }



    public void ReloadOrbit()
    {

        _numberOrbit = _ammo._clipSize ;
        _currentNumberOrbit = _numberOrbit;

        ReloadOrbit(_numberOrbit);
    }
    public void ReloadOrbit(int ammoNum)
    {
      
        for (int i = 0; i < ammoNum; i++)
        {
            GameObject go = Managers.Pool.Pop(_currentBullet, this.transform).gameObject;
            if (go != null)
            {
                OrbitBullet orbit = go.GetComponent<OrbitBullet>();
                if (orbit != null)
                {
                    Rigidbody rb = orbit.GetComponent<Rigidbody>();
                    rb.useGravity = false;
                    Vector3 orbitPos = GetInitOrbitPos(i);
                    orbit.SetOrbit(_characterController.transform, _orbitSpeed, orbitPos);
                    //orbit.transform.position = new Vector3(orbitPos.x, 1.0f, orbitPos.y);
                    _viewBullets.Enqueue(orbit.gameObject);
                }
            }

        }
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

    public void RefershOrbit()
    {
        float divideAngle = 360f / _currentNumberOrbit;
        while (_viewBullets.Count > 0)
        {
            GameObject go = _viewBullets.Dequeue();
            Poolable poolable = go.GetComponent<Poolable>();
            if (poolable)
            {
                Managers.Pool.Push(poolable);
            }
            else
            {
                Destroy(poolable.gameObject);
            }
        }

        _viewBullets.Clear();

        for (int i = 0; i < _currentNumberOrbit; i++)
        {
            GameObject go = Managers.Pool.Pop(_currentBullet, this.transform).gameObject;
            if (go != null)
            {
                OrbitBullet orbit = go.GetComponent<OrbitBullet>();
                if (orbit != null)
                {
                    Rigidbody rb = orbit.GetComponent<Rigidbody>();
                    rb.useGravity = false;
                    Vector3 orbitPos = GetOrbitPos(i);
                    orbit.SetOrbit(_characterController.transform, _orbitSpeed, orbitPos);
                    _viewBullets.Enqueue(orbit.gameObject);
                }
            }

        }

    }
    public GameObject GetOrbitBullet()
    {
        if (_viewBullets.Count > 0)
        {
            GameObject go = _viewBullets.Peek();
            _viewBullets.Dequeue();
            _currentNumberOrbit = _viewBullets.Count;
            return go;
        }

        return null;
    }
}
