using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionReloadState : ActionBaseState
{
    public override void EnterState(ActionStateManager actions)
    {
        actions._anim.SetTrigger("Reload");
        actions._beingAction = true;
    }

    public override void UpdateState(ActionStateManager actions)
    {

    }

  
}
