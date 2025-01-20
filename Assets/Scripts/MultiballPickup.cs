using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using static UnityEngine.ParticleSystem;
using Debug = System.Diagnostics.Debug;

public class MultiballPickup : MonoBehaviour
{
    public Vector3 spawnOffset =  Vector3.zero;
    private GameObject ball;
    public GameObject model;
    public ParticleSystem countdown;
    public Material newBallMaterial;

    public GameObject ballPrefab; 
    private Transform ballContainer;
    public float launchForce = 11f;
    private bool isAvailable = true;
    private const int numberOfSpawnedBalls = 1;
    void Start()
    {
        if (ballPrefab == null)
        {
            UnityEngine.Debug.LogError("No Ball prefab");
            return;
        }
        ball = GameObject.FindGameObjectWithTag("Ball");
    }

    void OnTriggerEnter(UnityEngine.Collider collision)
    {

        if (collision.gameObject.CompareTag("Ball") && isAvailable && collision.gameObject.GetComponent<Ball>().originalBall)
        {
            Debug.WriteLine("Hit");
            isAvailable = false;
            for (int i = 0; i < numberOfSpawnedBalls; i++)
            {
                StartCoroutine(DelayedSpawn());
            }
            StartCoroutine(DisablePickup());
        }
    }
    IEnumerator DelayedSpawn()
    {
        yield return new WaitForSeconds(0.5f);
        SpawnAndLaunchBall();
    }
        IEnumerator DisablePickup()
    {
        isAvailable = false;
        countdown.Play();
        model.GetComponentInChildren<Renderer>().enabled = false;
        yield return new WaitForSeconds(10f);
        isAvailable = true;
        countdown.Stop();
        countdown.Clear();
        model.GetComponentInChildren<Renderer>().enabled = true;

    }

    void SpawnAndLaunchBall()
    {
        GameObject newBall = Instantiate(ballPrefab, ball.transform.position, Quaternion.identity);
        newBall.GetComponent<Ball>().originalBall = false;

        Renderer ballRenderer = newBall.GetComponent<Renderer>();
        if (ballRenderer != null)
        {
            if (newBallMaterial != null)
                ballRenderer.material = newBallMaterial;
            else
                ballRenderer.material.color = Color.blue;

        }

        if (ballContainer != null)
        {
            newBall.transform.parent = ballContainer;
        }

        Rigidbody rb = newBall.GetComponent<Rigidbody>();
        if (spawnOffset != Vector3.zero) {
            newBall.transform.position += spawnOffset;
        }
        if (rb != null)
        {
            rb.AddForce(-transform.forward * launchForce, ForceMode.VelocityChange);
        }
    }
}
