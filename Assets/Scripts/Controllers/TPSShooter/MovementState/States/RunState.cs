using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : MovementBaseState
{
    public override void EnterState(MovementStateManager movement)
    {
        movement._anim.SetBool("Running", true);
    }

    public override void UpdateState(MovementStateManager movement)
    {
        if (Input.GetKeyUp(KeyCode.LeftShift)) ExitState(movement, movement._Walk);
        else if (movement._dir.magnitude < 0.1f) ExitState(movement, movement._Idle);
        else if(Input.GetKeyUp(KeyCode.Space)) ExitState(movement, movement._Dodge);

        if (movement._vInput < 0) movement._currentMoveSpeed = movement._RunBackSpeed;
        else movement._currentMoveSpeed = movement._RunSpeed;
    }

    void ExitState(MovementStateManager movement, MovementBaseState state)
    {
        movement._anim.SetBool("Running", false);
        movement.SwitchState(state);
    }
}
