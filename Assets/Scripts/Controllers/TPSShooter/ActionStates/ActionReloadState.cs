using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionReloadState : ActionBaseState
{
    public override void EnterState(ActionStateManager actions)
    {
        actions._anim.SetTrigger("Reload");
        actions._beingAction = true;
        CoroutineHelper.StartCoroutine(StartAction(actions));
    }

    public override void UpdateState(ActionStateManager actions)
    {

    }
    public void WeaponReloaded(ActionStateManager actions)
    {
        
        WeaponManager wm = actions.GetComponent<WeaponManager>();
        wm._currentWeapon.Reload();
        wm._AreloadWeapon?.Invoke();
        actions.SwitchState(actions._defaultState);
    }

    IEnumerator StartAction(ActionStateManager actions)
    {
        yield return new WaitForSeconds(1f);
        WeaponManager wm = actions.GetComponent<WeaponManager>();
        wm._currentWeapon.Reload();
        wm._AreloadWeapon?.Invoke();
        actions.SwitchState(actions._defaultState);
    }
}
