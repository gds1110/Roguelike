using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementStateManager : MonoBehaviour
{
    #region Movement
    public float _currentMoveSpeed;
    public float _walkSpeed=3, _walkBackSpeed=2;
    public float _RunSpeed=7, _RunBackSpeed=5;

    #endregion
    [HideInInspector] public Vector3 _dir;
    [HideInInspector] public float _hzInput, _vInput;
    CharacterController _characterController;


    [SerializeField] float _groundYOffest;
    [SerializeField] LayerMask _groundMask;
    Vector3 _spherePos;

    [SerializeField] float _gravity = -9.81f;
    Vector3 _velocity;


    MovementBaseState _currentState;
    public MovementBaseState _preState;
    public IdleState _Idle = new IdleState();
    public WalkState _Walk = new WalkState();
    public RunState _Run = new RunState();
    public DodgeState _Dodge = new DodgeState();

    [HideInInspector] public Animator _anim;

    private void Awake()
    {
        Managers.Game.SetPlayer(this.gameObject);
    }

    void Start()
    {
        _anim = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
        _walkSpeed = 3;
        _walkBackSpeed = 2;
        _RunSpeed = 7;
        _RunBackSpeed = 5;
        SwitchState(_Idle);
        // Managers.UI.MakeWorldSpaceUI<UI_HPBar>(transform);

        UI_Base hud_ui = Managers.UI.ShowSceneUI<UI_Scene>("HUD");
       if(hud_ui.GetComponent<UI_HUD>())
        {
            WeaponManager wm = this.GetComponent<WeaponManager>();
            hud_ui.GetComponent<UI_HUD>().InitWeaponUI(wm);
        }
    }

    // Update is called once per frame
    void Update()
    {
        GetDirectionAndMove();
        Gravity();

        _anim.SetFloat("hzInput", _hzInput);
        _anim.SetFloat("vInput", _vInput);

        _currentState.UpdateState(this);
    }

    public void SwitchState(MovementBaseState state)
    {
        if (_currentState != null)
        {
            _preState = _currentState;
        }
        _currentState = state;
        _currentState.EnterState(this);
    }

    void GetDirectionAndMove()
    {
        _hzInput = Input.GetAxis("Horizontal");
        _vInput = Input.GetAxis("Vertical");

        _dir = transform.forward * _vInput + transform.right * _hzInput;
        if(_currentState==_Dodge)
        {
            _dir = _Dodge._dir;
        }

        _characterController.Move(_dir * _currentMoveSpeed * Time.deltaTime);

    }
    bool IsGrounded()
    {
        _spherePos = new Vector3(transform.position.x,transform.position.y-_groundYOffest,transform.position.z);
        if (Physics.CheckSphere(_spherePos, _characterController.radius - 0.05f, _groundMask)) return true;
        return false;
    }

    void Gravity()
    {
        if (!IsGrounded()) _velocity.y += _gravity * Time.deltaTime;
        else if (_velocity.y < 0) _velocity.y = -2;

        _characterController.Move(_velocity * Time.deltaTime);
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if(_characterController)
           Gizmos.DrawWireSphere(_spherePos, _characterController.radius - 0.05f);
    }

}
