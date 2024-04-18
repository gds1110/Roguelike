using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitBullet : MonoBehaviour
{
    public Transform _target;
    public float _orbitSpeed=5;
    Vector3 _offSet;
    public bool _isOrbit = false;
    Vector3 _normalize;
    void Update()
    {
        if(_isOrbit==false)
        {
            return;
        }
        if (_target != null)
        {
            transform.position = _target.position + _offSet;
            transform.RotateAround(_target.position, Vector3.up, _orbitSpeed * Time.deltaTime);
            _offSet = transform.position - _target.position;
        }
    }

    public void TestSetOrbitPos(Vector3 pos,Transform target,float orbitSpeed)
    {
        transform.position = pos; //���� �� �Ÿ�
        _offSet = transform.position - target.position;
        _isOrbit = true;

    }

    public void SetOrbit(Transform target,float orbitSpeed,Vector3 offset)
    {
        transform.position = offset;
        _isOrbit = true;
        _offSet = transform.position - target.position;
        _orbitSpeed = orbitSpeed;
        _target = target;
    }
    public void SetOffset(Vector3 offset,Transform target)
    {
        _target = target;

        transform.position = offset;
        _offSet = transform.position - target.position;
        _isOrbit = true;
    }
  
}
