using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ActionStateManager : MonoBehaviour
{

    public ActionBaseState _currentState;

    public ActionReloadState _reloadState = new ActionReloadState();
    public ActionDefaultState _defaultState = new ActionDefaultState();

    public GameObject _currentWeapon;
    public WeaponAmmo _ammo;

    [HideInInspector] public Animator _anim;

    void Start()
    {
        SwitchState(_defaultState);
        _ammo = _currentWeapon.GetComponent<WeaponAmmo>();  
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _currentState.UpdateState(this);
    }

    public void SwitchState(ActionBaseState state)
    {
        _currentState = state;
        _currentState.EnterState(this);
    }
    public void WeaponReloaded()
    {
        _ammo.Reload();
        SwitchState(_defaultState);
    }
}
