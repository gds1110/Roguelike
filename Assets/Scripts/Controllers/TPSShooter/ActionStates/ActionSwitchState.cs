using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionSwitchState : ActionBaseState
{
    public override void EnterState(ActionStateManager actions)
    {
        actions._anim.SetTrigger("Switch");
        actions._beingAction = true;
    }

    public override void UpdateState(ActionStateManager actions)
    {
    }
}
