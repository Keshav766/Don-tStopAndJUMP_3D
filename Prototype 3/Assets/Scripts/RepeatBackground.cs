using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private Vector3 originalPosition;
    // private Vector3 moveLimit;
    private float moveLimit;
    void Start()
    {
        originalPosition = transform.position;
        // MeshTopology method
        // moveLimit = transform.position + new Vector3(-56,0,0);
        moveLimit = GetComponent<BoxCollider>().size.x / 2;
    }

   
    void Update()
    {
        if(transform.position.x < originalPosition.x - moveLimit)
        {
            transform.position = originalPosition;
        }
    }
}
