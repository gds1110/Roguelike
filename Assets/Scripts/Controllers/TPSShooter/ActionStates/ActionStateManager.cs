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

    public WeaponBased _currentWeapon;

    public bool _beingAction = false;
    WeaponManager wm;
    [HideInInspector] public Animator _anim;

    void Start()
    {
        SwitchState(_defaultState);
     
        _anim = GetComponent<Animator>();
         wm = GetComponent<WeaponManager>();
        _currentWeapon = wm._currentWeapon;
        
    }

    // Update is called once per frame
    void Update()
    {
        _currentState.UpdateState(this);
        if(wm._currentWeapon !=_currentWeapon)
        {
            _currentWeapon = wm._currentWeapon;
        }
    }

    public void SwitchState(ActionBaseState state)
    {
        _currentState = state;
        _currentState.EnterState(this);
    }

    // add to event at magicReload animation 
    public void WeaponReloaded()
    {
     
        WeaponManager wm = GetComponent<WeaponManager>();
        wm._currentWeapon.Reload();
        wm._AreloadWeapon?.Invoke();
        SwitchState(_defaultState);
    }

    // add to event at SwitchMagic animation 
    public void SwitchWeapon()
    {
        WeaponManager wm = GetComponent<WeaponManager>();
        SwitchState(_defaultState);
      
    }
}
