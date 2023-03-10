using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invisible_Walls : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if (transform.position.x > 10)
        {
            transform.position = new Vector3(10, transform.position.y, transform.position.z);
        }else if (transform.position.x < -10)
        {
            transform.position =  new Vector3(-10, transform.position.y, transform.position.z);
        }

        if (transform.position.z > 10)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 10);
        }else if (transform.position.z < -10)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        }
    }
}
