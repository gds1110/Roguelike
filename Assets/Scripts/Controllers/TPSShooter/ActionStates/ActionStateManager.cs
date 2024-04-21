using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ActionStateManager : MonoBehaviour
{

    public ActionBaseState _currentState;

    public ActionReloadState _reloadState = new ActionReloadState();
    public ActionDefaultState _defaultState = new ActionDefaultState();
    //switch weapon
    public ActionSwitchState _switchState = new ActionSwitchState();

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

    // add to event at magicReload animation 
    public void WeaponReloaded()
    {
        _ammo.Reload();
        _ammo._reloadAction.Invoke();
        SwitchState(_defaultState);
    }

    // add to event at SwitchMagic animation 
    public void SwitchWeapon()
    {
        WeaponManager wm = GetComponent<WeaponManager>();
        if(wm) wm.SwitchWeapon();
        SwitchState(_defaultState);
    }
}
