using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Movement : MonoBehaviour
{
    public float speed = 50;
    public Vector3 firstPosition;
    
    void Start()
    {
        firstPosition = transform.position;
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (transform.position.x > 10)
        {
            Destroy(gameObject);
        }else if (transform.position.x < -10)
        {
            Destroy(gameObject);
        }

        if (transform.position.z > 10)
        {
            Destroy(gameObject);
        }else if (transform.position.z < -10)
        {
            Destroy(gameObject);
        }
    }
}
