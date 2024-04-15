using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HipFireState : AimBaseState
{
    public override void EnterState(AimStateManager aim)
    {
        aim._anim.SetBool("Aiming", false);
        aim._CurrentFov = aim._hipFov;
    }

    public override void UpdateState(AimStateManager aim)
    {
        if (Input.GetKey(KeyCode.Mouse1)) aim.SwitchState(aim._aim);
    }

}
