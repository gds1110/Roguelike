using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : MovementBaseState
{
    public override void EnterState(MovementStateManager movement)
    {
        movement._anim.SetBool("Walking", true);
    }

    public override void UpdateState(MovementStateManager movement)
    {
        if (Input.GetKey(KeyCode.LeftShift))ExitState(movement,movement._Run);
        else if(movement._dir.magnitude<0.1f)ExitState(movement,movement._Idle);

        if (movement._vInput < 0) movement._currentMoveSpeed = movement._walkBackSpeed;
        else movement._currentMoveSpeed = movement._walkSpeed;
    }

    void ExitState(MovementStateManager movement, MovementBaseState state)
    {
        movement._anim.SetBool("Walking", false);
        movement.SwitchState(state);
    }

}
