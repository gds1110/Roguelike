using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraContoller : MonoBehaviour
{

    [SerializeField]
    Define.CmeraMode _mode = Define.CmeraMode.QuarterView;
    [SerializeField]
    Vector3 _delta = new Vector3(0.0f,6.0f,-5.0f);
    [SerializeField]
    GameObject _player = null;

    // Start is called before the first frame update
    void Start()
    {
        SetThirdView(new Vector3(0.5f, 1.8f, -1.5f));
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = _player.transform.position + _delta;
        transform.LookAt(transform.position);
    }

    public void SetQuaterView(Vector3 delta)
    {
        _mode = Define.CmeraMode.QuarterView;
        _delta = delta;
    }
    public void SetThirdView(Vector3 delta)
    {
        _mode = Define.CmeraMode.ThirdPersonView;
        _delta = delta;
 
    }

}
