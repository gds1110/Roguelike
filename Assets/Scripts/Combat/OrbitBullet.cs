using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

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


    public void SetOrbit(Transform target,float orbitSpeed,Vector3 pos)
    {
        transform.position = pos; //각도 및 거리
        _offSet = transform.position - target.position;
        _isOrbit = true;
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
