using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exporb : MonoBehaviour
{

    Vector3 startPos;
    public float offset=0.2f;
    public float delay=2.5f;

    public int exp = 1;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float offsetY = Mathf.Sin(Time.time * delay) * offset;
        transform.position = new Vector3(startPos.x,startPos.y+ offsetY, startPos.z);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerStat stat = other.gameObject.GetComponent<PlayerStat>();
            if (stat != null)
            {
                stat.Exp += exp;
            }

            Managers.Pool.Push(this.GetComponent<Poolable>());
        }
    }
}
