using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeJumper : MonoBehaviour
{
    PlayerLivesObject lives;
    private float velocityThreshold = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        lives = FindObjectOfType<PlayerLivesObject>();
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
            lives.Lives++;
        }
    }
}
