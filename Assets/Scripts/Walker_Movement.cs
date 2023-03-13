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
    private float timePassed = 2;
    private Animator walkerAnimation;
    private Bullet_Movement bullet;
    private float walkerHealt = 100.0f;
    private GameObject player;
    private float deadTimeCounter = 0f;


    void Start()
    {
        player = GameObject.Find("Player");
        walkerAttackScript = GetComponent<Walker_Attack>();
        walkerRb = GetComponent<Rigidbody>();
        walkerAnimation = GetComponent<Animator>();
    }

    void Update()
    {
        if (walkerAttackScript.isNearPlayer == false && walkerHealt > 0)
        {
            walkerAnimation.SetFloat("MoveSpeed", 2);
            
            if (timePassed < 0.5f)
            {
                timePassed += Time.deltaTime;
            }else
            {
                distanceX = player.transform.position.x - transform.position.x;
                distanceZ = player.transform.position.z - transform.position.z;
                timePassed = 0;
            }
            
            if (distanceX > upperDistanceBorder)
            {
                if (distanceZ > upperDistanceBorder) // sağ + üst
                {
                    walkerRb.velocity = new Vector3(speed, 0, speed);
                    transform.rotation = Quaternion.Euler(transform.rotation.x, 45, transform.rotation.z);
                }else if (distanceZ < lowerDistanceBorder) // sağ + alt
                {
                    walkerRb.velocity = new Vector3(speed, 0, -speed);
                    transform.rotation = Quaternion.Euler(transform.rotation.x, 135, transform.rotation.z);
                }else // sağ
                {
                    walkerRb.velocity = new Vector3(speed, 0, 0);
                    transform.rotation = Quaternion.Euler(transform.rotation.x, 90, transform.rotation.z);
                }
            }else if (distanceX < lowerDistanceBorder)
            {
                if (distanceZ > upperDistanceBorder) // sol + üst
                {
                    walkerRb.velocity = new Vector3(-speed, 0, speed);
                    transform.rotation = Quaternion.Euler(transform.rotation.x, 315, transform.rotation.z);
                }else if (distanceZ < lowerDistanceBorder) // sol + alt
                {
                    walkerRb.velocity = new Vector3(-speed, 0, -speed);
                    transform.rotation = Quaternion.Euler(transform.rotation.x, 225, transform.rotation.z);
                }else // sol
                {
                    walkerRb.velocity = new Vector3(-speed, 0, 0);
                    transform.rotation = Quaternion.Euler(transform.rotation.x, 270, transform.rotation.z);
                }
            }else
            {
                if (distanceZ > upperDistanceBorder) // üst
                {
                    walkerRb.velocity = new Vector3(0, 0, speed);
                    transform.rotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
                }else if (distanceZ < lowerDistanceBorder) // alt
                {
                    walkerRb.velocity = new Vector3(0, 0, -speed);
                    transform.rotation = Quaternion.Euler(transform.rotation.x, 180, transform.rotation.z);
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
            
            if (deadTimeCounter < 3)
            {
                deadTimeCounter += Time.deltaTime;
            }else{
                Destroy(gameObject);
            }
        }
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
                walkerRb.AddForce(vector3X.normalized * 50, ForceMode.Impulse);
            }else
            {
                Vector3 vector3Z = new (0.0f, 0.0f, bulletWalkerDistanceZ);
                walkerRb.AddForce(vector3Z.normalized * 50, ForceMode.Impulse);
            
            }
            
            Destroy(other.gameObject);
        }
    }

    /*private void TrackAndWalk() (not able to use)
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