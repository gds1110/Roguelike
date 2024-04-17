using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    // Start is called before the first frame update

    public Define.WorldObject WorldObjectType { get; private set; } = Define.WorldObject.Unknown;   

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
