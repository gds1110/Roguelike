using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionSwitchState : ActionBaseState
{
    public override void EnterState(ActionStateManager actions)
    {
        actions._anim.SetTrigger("Switch");
        actions._beingAction = true;
        CoroutineHelper.StartCoroutine(StartAction(actions));

    }

    public override void UpdateState(ActionStateManager actions)
    {
    }


    // add to event at SwitchMagic animation 
    public void SwitchWeapon(ActionStateManager actions)
    {
        WeaponManager wm = actions.GetComponent<WeaponManager>();
        actions.SwitchState(actions._defaultState);

    }
    IEnumerator StartAction(ActionStateManager actions)
    {
        yield return new WaitForSeconds(0.3f);
        WeaponManager wm = actions.GetComponent<WeaponManager>();
        wm.SwitchWeapon();
        actions.SwitchState(actions._defaultState);
    }
}
