using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitBullet : MonoBehaviour
{
    public Transform _target;
    public float _orbitSpeed=5;
    Vector3 _offSet;
    public bool _isOrbit = false;

    void Start()
    {

    
    }
    private void OnEnable()
    {

    }
    private void OnDisable()
    {
        _isOrbit = false;
    }

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

    public void SetOrbit(Transform target,float orbitSpeed,Vector3 offset)
    {
        transform.position = offset;
        _isOrbit = true;
        _offSet = transform.position - target.position;
        _orbitSpeed = orbitSpeed;
        _target = target;
    }

  
}
