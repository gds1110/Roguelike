using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Timeline.Actions;

public class DodgeState : MovementBaseState
{

    public Vector3 _dir;
    public bool _isStart=false;
    public float _elapsTime;
    public float _maxTime;

    public override void EnterState(MovementStateManager movement)
    {
        _dir = movement._dir;
        movement._currentMoveSpeed *= 2;
        movement._anim.SetTrigger("Dodge");
        _elapsTime = 0;
        _isStart = true;
        _maxTime = 0.5f;
    }

    public override void UpdateState(MovementStateManager movement)
    {
        if(_isStart)
        {
            _elapsTime += Time.deltaTime; 

            if(_elapsTime > _maxTime )
            {

                ExitState(movement, movement._preState);
            }
        }
    }
    void ExitState(MovementStateManager movement, MovementBaseState state)
    {
        _elapsTime = 0;
        _isStart = false;
        movement._currentMoveSpeed *= 0.5f;
        movement.SwitchState(state);
    }



    //void Dodge()
    //{
    //    if (_jDown && _moveVector != Vector3.zero && bIsGround && !_isDodge)
    //    {
    //        _dodgeVector = _moveVector;
    //        _speed *= 2;
    //        _anim.SetTrigger("doDodge");
    //        _isDodge = true;

    //        Invoke("DodgeOut", 0.4f);
    //    }
    //}
    //void DodgeOut()
    //{
    //    _speed *= 0.5f;
    //    _isDodge = false;
    //}
}
