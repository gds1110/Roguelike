using Cinemachine;
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
    [HideInInspector] public CinemachineVirtualCamera _vCam;
    public float _adsFov = 40;
    [HideInInspector] public float _hipFov;
    [HideInInspector] public float _CurrentFov;
    public float _foveSmoothSpeed= 10;

    public Transform _aimPos;
    [HideInInspector] public Vector3 actualAimPos;
    [SerializeField] float _aimSmoothSpeed=20;
    [SerializeField] LayerMask _aimMask;

    UI_CrossHair _crossHair;

    // Start is called before the first frame update
    void Start()
    {

        _vCam = GetComponentInChildren<CinemachineVirtualCamera>();
        _hipFov = _vCam.m_Lens.FieldOfView;
        _anim = GetComponent<Animator>();   

        SwitchState(_hip);

        _crossHair = Managers.UI.ShowSceneUI<UI_CrossHair>();

    }

    // Update is called once per frame
    void Update()
    {
        _xAxis += Input.GetAxisRaw("Mouse X")*_mouseSense;
        _yAxis -= Input.GetAxisRaw("Mouse Y")*_mouseSense;
        _yAxis = Mathf.Clamp(_yAxis, -80f, 80f);

        _vCam.m_Lens.FieldOfView = Mathf.Lerp(_vCam.m_Lens.FieldOfView, _CurrentFov, _foveSmoothSpeed * Time.deltaTime);

        Vector2 screenCenter = new Vector2(Screen.width/2, Screen.height/2);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _aimMask))
        {
            _aimPos.position = Vector3.Lerp(_aimPos.position, hit.point, _aimSmoothSpeed * Time.deltaTime);
           
        }


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
