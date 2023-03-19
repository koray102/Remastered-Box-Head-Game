using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public float speed = 20.0f;
    public GameObject bulletPrefab;
    public float playerHealth = 100f;
    private Animator playerAnimator;
    
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        // transform = a function which enable us to change the values such as position, rotation, scale.
        // translate = keeps position's value.
        if (playerHealth > 0)
        {
            // movement
            if (Input.GetKey(KeyCode.RightArrow))
            {
                RotateForRightAndLeft(45, 135, 90);
            }else if (Input.GetKey(KeyCode.LeftArrow))
            {
                RotateForRightAndLeft(315, 225, 270);
            }else if (Input.GetKey(KeyCode.UpArrow))
            {
                RotateForUpAndDown(0);
            }else if (Input.GetKey(KeyCode.DownArrow))
            {
                RotateForUpAndDown(180);
            }else
            {
                playerAnimator.SetFloat("Speed_f", 0);
            }
            
            // firing bullet
            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerAnimator.SetInteger("WeaponType_int", 4);
                playerAnimator.SetBool("Shoot_b", true);
                
                Instantiate (bulletPrefab, new Vector3 (transform.position.x, 0.92f, transform.position.z), transform.rotation);
            }else
            {
                playerAnimator.SetBool("Shoot_b", false);
            }
        }else
        {
            playerAnimator.SetBool("Death_b", true);
            playerAnimator.SetInteger("DeathType_int", 1);
            playerAnimator.SetInteger("WeaponType_int", 100);
        }
    }

    void RotateForRightAndLeft(int YRotateU, int YRotateD, int YRotate)
    {
        if(Input.GetKey(KeyCode.UpArrow)) //... + up
        {
            transform.rotation = Quaternion.Euler(0, YRotateU, 0);
        }else if (Input.GetKey(KeyCode.DownArrow)) //... + down
        {
            transform.rotation = Quaternion.Euler(0, YRotateD, 0);
        }else 
        {
            transform.rotation = Quaternion.Euler(0, YRotate, 0);
        }
        
        playerAnimator.SetFloat("Speed_f", 1);
        transform.Translate(Vector3.forward * Time.deltaTime * speed);      
    }

    void RotateForUpAndDown(int YRotate)
    {
        transform.rotation = Quaternion.Euler(0, YRotate, 0);

        playerAnimator.SetFloat("Speed_f", 1);
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }
}