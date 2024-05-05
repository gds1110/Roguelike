using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : MovementBaseState
{
    public override void EnterState(MovementStateManager movement)
    {
      

    }

    public override void UpdateState(MovementStateManager movement)
    {
        if(movement._dir.magnitude>0.1f)
        {
            if (Input.GetKey(KeyCode.LeftShift)) movement.SwitchState(movement._Run);
            else movement.SwitchState(movement._Walk);
        }
    }
}
