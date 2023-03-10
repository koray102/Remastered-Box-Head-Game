using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walker_Attack : MonoBehaviour
{
    public float attackDelay = 1.0f;
    public float attackInterval = 2.0f;
    private Player_Movement playerMovementScript;
    public bool isNearPlayer = false;
    public float timePassed = 0f;
    private Animator walkerAnimation;
    private Vector3 playersPlace;

    void Start()
    {
        
    }

    void Update()
    {
        playerMovementScript = GameObject.Find("Player").GetComponent<Player_Movement>();
        walkerAnimation = GetComponent<Animator>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.name == "Player")
        {
            isNearPlayer = true;
            
            if (timePassed < 2.5f)
            {
                timePassed += Time.deltaTime;
            }else
            {               
                walkerAnimation.SetTrigger("Attack");
                playerMovementScript.playerHealth -= 25;
                Debug.Log(playerMovementScript.playerHealth);
                timePassed = 0;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.name == "Player")
        {
            isNearPlayer = false;
            timePassed = 0;
        }
    }
}
