using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileRotate : MonoBehaviour
{
    public bool _isRotating = false;
    private void Update()
    {
        if (_isRotating)
            transform.Rotate(Vector3.right * 30 * Time.deltaTime);
    }
}
