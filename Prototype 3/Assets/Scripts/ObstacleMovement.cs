using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    public float obstacleSpeed = 10f;
    public float xLimitLeft = -10f;

    private PlayerController playerControllerScript;

    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }


    void Update()
    {
        if (!playerControllerScript.gameOver)
        {
            transform.Translate(Vector3.left * Time.deltaTime * obstacleSpeed);
        }

        if (gameObject.CompareTag("Obstacle") && transform.position.x < xLimitLeft)
        {
            Destroy(gameObject);
        }

    }
}
