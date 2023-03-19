using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walker_Movement : MonoBehaviour
{
    public float speed = 3.5f;
    public float upperDistanceBorder = 0f;
    public float lowerDistanceBorder = 0f;
    public float distanceX;
    public float distanceZ;
    public Collider walkerCollider;
    private Rigidbody walkerRb;
    private Walker_Attack walkerAttackScript;
    private Animator walkerAnimation;
    private Bullet_Movement bullet;
    private float walkerHealt = 100.0f;
    private GameObject player;


    void Start()
    {
        player = GameObject.Find("Player");
        walkerAttackScript = GetComponent<Walker_Attack>();
        walkerRb = GetComponent<Rigidbody>();
        walkerAnimation = GetComponent<Animator>();

        StartCoroutine("CalculateDistance");
    }

    void Update()
    {
        if (walkerAttackScript.isNearPlayer == false && walkerHealt > 0)
        {
            walkerAnimation.SetFloat("MoveSpeed", 2);
            
            if (distanceX > upperDistanceBorder)
            {
                if (distanceZ > upperDistanceBorder) // right + up
                {
                    VelocityAndRotation(speed, speed, 45);
                }else if (distanceZ < lowerDistanceBorder) // right + down
                {
                    VelocityAndRotation(speed, -speed, 135);
                }else // right
                {
                    VelocityAndRotation(speed, 0, 90);
                }
            }else if (distanceX < lowerDistanceBorder)
            {
                if (distanceZ > upperDistanceBorder) // left + up
                {
                    VelocityAndRotation(-speed, speed, 315);
                }else if (distanceZ < lowerDistanceBorder) // left + down
                {
                    VelocityAndRotation(-speed, -speed, 225);
                }else // left
                {
                    VelocityAndRotation(-speed, 0, 270);
                }
            }else
            {
                if (distanceZ > upperDistanceBorder) // up
                {
                    VelocityAndRotation(0, speed, 0);
                }else if (distanceZ < lowerDistanceBorder) // down
                {
                    VelocityAndRotation(0, -speed, 180);
                }
            }
        
        }else if (walkerHealt > 0)
        {
            walkerRb.velocity = new Vector3(0, 0, 0);
            walkerAnimation.SetFloat("MoveSpeed", 0);
        }

        if (walkerHealt < 0)
        {
            walkerAnimation.SetTrigger("Dead");
            walkerRb.velocity = new Vector3(0, 0, 0);
            walkerCollider.enabled = false;
            
            StartCoroutine("WaitWalkerDeath");
        }
    }

    void VelocityAndRotation(float speedX, float speedZ, int rotationY)
    {
        walkerRb.velocity = new Vector3(speedX, 0, speedZ);
        transform.rotation = Quaternion.Euler(transform.rotation.x, rotationY, transform.rotation.z);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            walkerHealt -= 33;
            
            bullet = other.gameObject.GetComponent<Bullet_Movement>();
            
            float bulletWalkerDistanceX = transform.position.x - bullet.firstPosition.x;
            float bulletWalkerDistanceZ = transform.position.z - bullet.firstPosition.z;
            
            if (Mathf.Abs(bulletWalkerDistanceX) > Mathf.Abs(bulletWalkerDistanceZ))
            {
                Vector3 vector3X = new (bulletWalkerDistanceX, 0.0f, 0.0f);
                walkerRb.AddForce(vector3X.normalized * 30, ForceMode.Impulse);
            }else
            {
                Vector3 vector3Z = new (0.0f, 0.0f, bulletWalkerDistanceZ);
                walkerRb.AddForce(vector3Z.normalized * 30, ForceMode.Impulse);
            }
            
            Destroy(other.gameObject);
        }
    }

    IEnumerator WaitWalkerDeath()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }

    IEnumerator CalculateDistance()
    {
        while (walkerHealt > 0)
        {
            distanceX = player.transform.position.x - transform.position.x;
            distanceZ = player.transform.position.z - transform.position.z;
            yield return new WaitForSeconds(1);
        }
    }

    /*private void TrackAndWalk() (didn't work but can be improved)
    {
        private float angleBetween;
        private float rotationForY;
        private Vector3 lastPosition;
        
        angleBetween = Vector3.SignedAngle((player.transform.position - transform.position), Vector3.forward, Vector3.up);
                if (90 > angleBetween && angleBetween > 0)
                {
                    rotationForY = 315;
                }else if(180 > angleBetween && angleBetween > 90)
                {
                    rotationForY = 225;
                }else if (0 > angleBetween && angleBetween > -90)
                {
                    rotationForY = 45;
                }else if(-90 > angleBetween && angleBetween > -180)
                {
                    rotationForY = 135;
                }else
                {
                    rotationForY = -angleBetween;
                }
                transform.rotation = Quaternion.Euler(transform.rotation.x, rotationForY, transform.rotation.z);

                if (distanceX > upperDistanceBorder)
                {
                    distanceX = speed;
                }else if (distanceX < lowerDistanceBorder)
                {
                    distanceX = - speed;
                }else
                {
                    distanceX = 0;
                }

                if (distanceZ > upperDistanceBorder)
                {
                    distanceZ = speed;
                }else if (distanceZ < lowerDistanceBorder)
                {
                    distanceZ = - speed;
                }else
                {
                    distanceZ = 0;
                }
                walkerRb.velocity = new Vector3(distanceX, 0, distanceZ);
    }*/
}