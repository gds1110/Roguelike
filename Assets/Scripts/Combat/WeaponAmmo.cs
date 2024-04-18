using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAmmo : MonoBehaviour
{
    public int _clipSize;
    public int _extraAmmo;
    public int _currentAmmo;
    public bool _isUnlimit = true;

    public Action _reloadAction=null;

    // Start is called before the first frame update
    void Start()
    {
        _currentAmmo = _clipSize;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) {
            Reload();
            _reloadAction.Invoke();
        }
    }

    public void Reload()
    {
        if (_isUnlimit == true)
        {
            _currentAmmo = _clipSize;
        }
        else
        {
            if (_extraAmmo >= _clipSize)
            {
                int ammoToReload = _clipSize - _currentAmmo;
                _extraAmmo -= ammoToReload;
                _currentAmmo += ammoToReload;
            }
            else if(_extraAmmo>0)
            {
                if(_extraAmmo+_currentAmmo>_clipSize)
                {
                    int leftOverAmmo = _extraAmmo + _currentAmmo - _clipSize;
                    _extraAmmo = leftOverAmmo;
                    _currentAmmo = _clipSize;
                }
                else
                {
                    _currentAmmo += _extraAmmo;
                    _extraAmmo = 0;
                }
            }
        }
    }
}
