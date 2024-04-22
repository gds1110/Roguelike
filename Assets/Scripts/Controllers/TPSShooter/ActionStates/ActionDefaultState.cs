using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionDefaultState : ActionBaseState
{
    public override void EnterState(ActionStateManager actions)
    {
    }

    public override void UpdateState(ActionStateManager actions)
    {

        if(Input.GetKeyDown(KeyCode.R)&&CanReload(actions))
        {
            actions.SwitchState(actions._reloadState);
            Managers.Sound.Play("Reload");
        }
        if(Input.GetKeyDown(KeyCode.T)&&CanSwitch(actions))
        {
            actions.SwitchState(actions._switchState);
            Managers.Sound.Play("Reload");
        }
    }

    bool CanReload(ActionStateManager actions)
    {
 
        if (actions._currentWeapon._currentAmmo == actions._currentWeapon._ammoSize) return false;
        else return true;

    }
    bool CanSwitch(ActionStateManager actions)
    {

        return true;
    }
}
