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
    }

    bool CanReload(ActionStateManager actions)
    {

        if (actions._ammo._isUnlimit == true) return true;
        if (actions._ammo._currentAmmo == actions._ammo._clipSize) return false;
        else if (actions._ammo._extraAmmo == 0) return false;
        else return true;

    }
}
