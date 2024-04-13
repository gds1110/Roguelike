using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    PlayerStat _stat;

    [SerializeField]
    float _jumpForce = 3f;
    [SerializeField]
    float _dash = 5f;
    [SerializeField]    
    float _speed = 10.0f;
    [SerializeField]
    float rotSpeed = 2f;
    private Vector3 _dir = Vector3.zero;

    Vector3 _moveVector;
    Vector3 _dodgeVector;
    bool _wDown;
    bool _jDown;
    bool _isDodge;

    private bool bIsGround = false;
    public LayerMask layer;

    PlayerState _state;

    Animator _anim;
    public enum PlayerState
    {
        Die,
        Moving,
        Idle,
        Channeling,
        Jumping,
        Falling,
        Skill,
    }
    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    void Start()
    {
        _stat = gameObject.GetOrAddComponent<PlayerStat>(); 

        //Managers.Input.KeyAction -= OnKeyboard;
        //Managers.Input.KeyAction += OnKeyboard;
        
        rb = GetComponent<Rigidbody>();
        _state = PlayerState.Idle;
    }

    void Update()
    {

        switch(_state)
        {
            case PlayerState.Idle:
                UpdateIdle();
                break;
            case PlayerState.Moving:
                UpdateMoving();
                break;
            case PlayerState.Jumping:
                UpdateJumping();
                break;

        }

    }
    private void FixedUpdate()
    {
        OnKeyboard();
        Move();
        Turn();
        Jump();
        Dodge();
    }
    void UpdateMoving()
    {
        if (_moveVector==Vector3.zero)
        {
            _state = PlayerState.Idle;
        }
    }
    void UpdateIdle()
    {

    }
    void UpdateJumping()
    {
        CheckGround();
    }
    void OnKeyboard()
    {
        _dir.x = Input.GetAxis("Horizontal");
        _dir.z = Input.GetAxis("Vertical");
        _wDown = Input.GetButton("Walk");
        _jDown = Input.GetButton("Jump");
   
        _moveVector = new Vector3(_dir.x,0,_dir.z).normalized;
        if(_isDodge)
        {
            _moveVector = _dodgeVector;
        }

        _anim.SetBool("isRun", _moveVector != Vector3.zero);
        _anim.SetBool("isWalk", _wDown);
        if (_moveVector != Vector3.zero)
        {
            _state = PlayerState.Moving;

        }

        CheckGround();

    }
    int _mask = (1 << (int)Define.Layer.Ground | (1 << (int)Define.Layer.Monster));

    void CheckGround()
    {
        RaycastHit hit;

        if(Physics.Raycast(transform.position+(Vector3.up*0.2f),Vector3.down,out hit,0.4f,layer))
        {
            bIsGround = true;
            Animator anim = GetComponent<Animator>();
            anim.SetBool("isGround", bIsGround);
            _anim.SetBool("isJump", false);

        }
        else
        {
            bIsGround= false;
        }
      //  _moveToDest = false;
    }
    void Move()
    {
        transform.position += _moveVector * _speed * (_wDown ? 0.3f : 1f) * Time.deltaTime;
    }
    void Turn()
    {
        transform.LookAt(transform.position + _moveVector);
    }
    void Jump()
    {

        if (_jDown && _moveVector == Vector3.zero&&bIsGround&&!_isDodge)
        {
            _anim.SetBool("isJump", true);
            _anim.SetTrigger("doJump");

            Vector3 jumpPower = Vector3.up * _jumpForce;

            rb.AddForce(jumpPower, ForceMode.Impulse);
            //rb.velocity = jumpPower;
            //_anim.Play("JUMP");
            _state = PlayerState.Jumping;
        }
    }
    void Dodge()
    {
        if(_jDown&& _moveVector != Vector3.zero&&bIsGround&&!_isDodge)
        {
            _dodgeVector = _moveVector;
            _speed *= 2;
            _anim.SetTrigger("doDodge");
            _isDodge = true;

            Invoke("DodgeOut", 0.4f);
        }
    }
    void DodgeOut()
    {
        _speed *= 0.5f;
        _isDodge = false;
    }
    void OnMouseClicked(Define.MouseEvent evt)
    {
        if (evt != Define.MouseEvent.Click)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if(Physics.Raycast(ray,out hit,100.0f,LayerMask.GetMask("Ground")))
        {

        }
        
    }

    void OnClickedMove()
    {

    }
}
