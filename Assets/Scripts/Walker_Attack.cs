using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walker_Attack : MonoBehaviour
{
    public bool isNearPlayer = false;
    public float timePassed = 0f;
    public float attackFrequency = 2f;
    private Animator walkerAnimation;
    private Player_Movement playerMovementScript;

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

    /*IEnumerator Attack() (didn't work but can be improved)
    {
        while (isNearPlayer == true)
        {
            yield return new WaitForSeconds(attackFrequency);
            if (isNearPlayer == true)
            {
                walkerAnimation.SetTrigger("Attack");
                playerMovementScript.playerHealth -= 25;
                Debug.Log(playerMovementScript.playerHealth);
            }
        }
    }*/
}
