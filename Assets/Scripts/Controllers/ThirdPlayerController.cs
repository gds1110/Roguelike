using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPlayerController : MonoBehaviour
{

    Animator _anim;
    Camera _camera;
    CharacterController _characterController;

    public float _speed = 5f;
    public float _runSpeed = 8f;
    public float _finalSpeed = 0;
    public bool toggleCameraRotation;
    public bool _isRun;
    public float _smoothness = 10f;
    private bool _wDown;

    void Start()
    {
        _anim = GetComponent<Animator>();
        _camera = Camera.main;
        _characterController = GetComponent<CharacterController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftAlt))
        {
            toggleCameraRotation = true;
        }
        else
        {
            toggleCameraRotation = false;
        }
        _wDown = Input.GetButton("Walk");

        InputMovement();
    }

    private void LateUpdate()
    {
        if(toggleCameraRotation!= true)
        {
            Vector3 playerRotate = Vector3.Scale(_camera.transform.forward, new Vector3(1, 0, 1));
            transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(playerRotate),Time.deltaTime*_smoothness);
        }
    }

    void InputMovement()
    {
        _finalSpeed = (_wDown) ? _runSpeed : _speed;

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        Vector3 moveDirection = forward * Input.GetAxisRaw("Vertical") + right * Input.GetAxisRaw("Horizontal");
        _characterController.Move(moveDirection.normalized * _finalSpeed * Time.deltaTime);
    }

}
