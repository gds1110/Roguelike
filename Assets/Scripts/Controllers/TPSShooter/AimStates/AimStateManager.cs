using NUnit.Framework.Internal.Execution;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimStateManager : MonoBehaviour
{

    AimBaseState _currentState;
    public HipFireState _hip = new HipFireState();
    public AimState _aim = new AimState();
    

    [SerializeField]
    float _mouseSense = 1;
    public float _xAxis;
    public float _yAxis;
    [SerializeField] Transform _camFollowPos;


    [HideInInspector] public Animator _anim;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();   

        SwitchState(_hip);
    }

    // Update is called once per frame
    void Update()
    {
        _xAxis += Input.GetAxisRaw("Mouse X")*_mouseSense;
        _yAxis -= Input.GetAxisRaw("Mouse Y")*_mouseSense;
        _yAxis = Mathf.Clamp(_yAxis, -80f, 80f);

        _currentState.UpdateState(this);
    }

    private void LateUpdate()
    {
        _camFollowPos.localEulerAngles = new Vector3(_yAxis, _camFollowPos.localEulerAngles.y, _camFollowPos.localEulerAngles.z);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, _xAxis, transform.eulerAngles.z);

    }

    public void SwitchState(AimBaseState state)
    {
        _currentState = state;
        _currentState.EnterState(this);
    }

}
