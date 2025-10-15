using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject ball;          // The ball to follow
    public float lerpRate = 5f;      // Smoothness of the camera follow
    public bool gameOver = false;    // Set to true when game ends

    private Vector3 offset;

    void Start()
    {
        if (ball != null)
        {
            offset = ball.transform.position - transform.position;
        }
        else
        {
            Debug.LogError("Ball is not assigned to CameraFollow!");
        }
    }

    void Update()
    {
        if (!gameOver && ball != null)
        {
            Follow();
        }
    }

    void Follow()
    {
        Vector3 targetPos = ball.transform.position - offset;
        transform.position = Vector3.Lerp(transform.position, targetPos, lerpRate * Time.deltaTime);
    }
}
