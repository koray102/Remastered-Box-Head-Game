using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walker_Spawner : MonoBehaviour
{
    public GameObject WalkerPrefab;
    private int walkerAmount;
    private int walkerCount;
    
    void Start()
    {

    }

    void Update()
    {
        walkerCount = FindObjectsOfType<Walker_Movement>().Length;

        if (walkerCount == 0)
        {
            walkerAmount += 3;
            StartCoroutine(spawnWalker());
        }
    }
    
    IEnumerator spawnWalker()
    {
        for(int i = 0; i < walkerAmount; i++)
        {
            Instantiate(WalkerPrefab, new Vector3(Random.Range(0.5f, -0.5f), 0, -9.5f), transform.rotation);
            Instantiate(WalkerPrefab, new Vector3(Random.Range(0.5f, -0.5f), 0, 9.5f), transform.rotation);
            yield return new WaitForSeconds(1.5f);
        }
    }
}
