using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class MultiballJumper : MonoBehaviour
{
    Vector3 startingPosition;
    PlayerLivesObject lives;
    private GameObject ball;

    public GameObject ballPrefab; 
    private Transform ballContainer;
    public float launchForce = 100f;

    private float velocityThreshold = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        if (ballPrefab == null)
        {
            UnityEngine.Debug.LogError("No Ball prefab");
            return;
        }

        lives = FindObjectOfType<PlayerLivesObject>();
        ball = GameObject.FindGameObjectWithTag("Ball");
        startingPosition = ball.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        float impactStrength = collision.relativeVelocity.magnitude;
        if (collision.gameObject.CompareTag("Ball") && collision.gameObject.GetComponent<Ball>().originalBall && impactStrength > velocityThreshold)
        {
            for (int i = 0; i < 3; i++)
            {
                SpawnAndLaunchBall();
            }
        }
    }

    void SpawnAndLaunchBall()
    {
        GameObject newBall = Instantiate(ballPrefab, startingPosition, Quaternion.identity);
        newBall.GetComponent<Ball>().originalBall = false;

        Renderer ballRenderer = newBall.GetComponent<Renderer>();
        if (ballRenderer != null)
        {
            ballRenderer.material.color = Color.blue;
        }

        if (ballContainer != null)
        {
            newBall.transform.parent = ballContainer;
        }

        Rigidbody rb = newBall.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(transform.forward * launchForce, ForceMode.VelocityChange);
        }
    }
}
