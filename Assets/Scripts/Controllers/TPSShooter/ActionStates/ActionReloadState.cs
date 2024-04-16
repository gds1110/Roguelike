using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionReloadState : ActionBaseState
{
    public override void EnterState(ActionStateManager actions)
    {
        actions._anim.SetTrigger("Reload");
    }

    public override void UpdateState(ActionStateManager actions)
    {

    }

  
}
