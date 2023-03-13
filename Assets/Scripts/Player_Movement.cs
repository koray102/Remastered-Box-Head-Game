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
        // transform = konum, rotasyon, boyut değerlerini değiştirmeye yarayan fonksiyon.
        // translate = konum değerini tutar.
        if (playerHealth > 0)
        {
            // hareket
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                playerAnimator.SetFloat("Speed_f", 1);
                if(Input.GetKey(KeyCode.DownArrow)) //sol + alt
                {
                    transform.rotation = Quaternion.Euler(0, 225, 0);
                }else if (Input.GetKey(KeyCode.UpArrow)) //sol + üst
                {
                    transform.rotation = Quaternion.Euler(0, 315, 0);
                }else //sol
                {
                    transform.rotation = Quaternion.Euler(0, 270, 0);
                }
                transform.Translate(Vector3.forward * Time.deltaTime * speed);
            }else if (Input.GetKey(KeyCode.RightArrow))
            {
                playerAnimator.SetFloat("Speed_f", 1);
                if(Input.GetKey(KeyCode.DownArrow)) //sağ + alt
                {
                    transform.rotation = Quaternion.Euler(0, 135, 0);
                }else if (Input.GetKey(KeyCode.UpArrow)) //sağ + üst
                {
                    transform.rotation = Quaternion.Euler(0, 45, 0);
                }else //sağ
                {
                    transform.rotation = Quaternion.Euler(0, 90, 0);
                }
                transform.Translate(Vector3.forward * Time.deltaTime * speed);
            }else if (Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
            {
                playerAnimator.SetFloat("Speed_f", 1);
                transform.rotation = Quaternion.Euler(0, 180, 0);
                transform.Translate(Vector3.forward * Time.deltaTime * speed);
            }else if (Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
            {
                playerAnimator.SetFloat("Speed_f", 1);
                transform.rotation = Quaternion.Euler(0, 0, 0);
                transform.Translate(Vector3.forward * Time.deltaTime * speed);
            }else
            {
                playerAnimator.SetFloat("Speed_f", 0);
            }
            
            //mermi ateşleme
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
}