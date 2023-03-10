using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossbow_Movement : MonoBehaviour
{
    public Player_Movement PlayerMovementSc;
    public Rigidbody crossbowRb;
    private Animator crossbowAnimator;
    private bool isCrossbowAimStraight = true;
    private bool isCrossbowAimShoot = false;
    private float crossbowCounter = 0f;
    private float crssbowMoveCounter;

    void Start()
    {
        crossbowAnimator = GameObject.Find("Crossbow").GetComponent<Animator>();

    }

    void Update()
    {
        if (PlayerMovementSc.playerHealth > 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                crossbowAnimator.SetBool("Fire", true);
            }
            crossbowAnimator.SetBool("Fire", false);
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.UpArrow))
        {
            if (isCrossbowAimStraight == true)
            {
                transform.Rotate(Vector3.forward * -10);
                isCrossbowAimStraight = false;
            }
        }else if (isCrossbowAimStraight == false)
        {
            transform.Rotate(Vector3.forward * 10);
            isCrossbowAimStraight = true;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isCrossbowAimShoot == false)
        {
            transform.Rotate(Vector3.forward * 13);
            isCrossbowAimShoot = true;
        }else if (isCrossbowAimShoot == true)
        {
            if (crossbowCounter < 0.3f)
            {
                //Debug.Log("if");
                crossbowCounter += Time.deltaTime;
            }else if (crossbowCounter > 0.3f)
            {
                transform.Rotate(Vector3.forward * -13);
                crossbowCounter = 0;
                isCrossbowAimShoot = false;
            }
        }
    }
}
