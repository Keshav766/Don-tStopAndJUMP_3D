using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagre : MonoBehaviour
{
    public GameObject[] obstaclePrefab;

    public Vector3 spawnPosition = new Vector3(30, 0, 0);
    public float startDelay = 2f;
    public float repeatDelay = 2f;

    private PlayerController playerControllerScript;

    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        InvokeRepeating("SpawnFence", startDelay, repeatDelay);
    }

    void Update()
    {

    }

    void SpawnFence()
    {
        int index = Random.Range(0, obstaclePrefab.Length);
        if (!playerControllerScript.gameOver)
        {
            Instantiate(obstaclePrefab[index], spawnPosition, transform.rotation);
        }
    }
}
