using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using static UnityEngine.ParticleSystem;
using Debug = System.Diagnostics.Debug;

public class LivesPickup : MonoBehaviour
{
    PlayerLivesObject lives;
    public GameObject model;
    public ParticleSystem countdown;
    private bool isAvailable = true;
    void Start()
    {

        lives = FindObjectOfType<PlayerLivesObject>();
    }

    void OnTriggerEnter(UnityEngine.Collider collision)
    {

        if (collision.gameObject.CompareTag("Ball") && isAvailable && collision.gameObject.GetComponent<Ball>().originalBall)
        {
            Debug.WriteLine("Hit");
            isAvailable = false;
            lives.Lives++;
            StartCoroutine(DisablePickup());
        }
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
}
