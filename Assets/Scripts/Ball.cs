using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [Header("Ball Collision Sound Settings")]
    [Tooltip("The sound played when the ball hits something.")]
    [SerializeField] private AudioClip hitSound;

    [Tooltip("Minimum velocity magnitude for sound playback.")]
    [Range(0f, 1f)]
    [SerializeField] private float velocityThreshold = 0.1f;
    
    [Tooltip("Maximum volume for the hit sound.")]
    [Range(1f, 10f)]
    [SerializeField] private float maxVolume = 1f;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        float impactStrength = collision.relativeVelocity.magnitude;

        if (impactStrength > velocityThreshold)
        {
            float volume = Mathf.Clamp(impactStrength / 10f, 0f, maxVolume);

            audioSource.PlayOneShot(hitSound, volume);
        }
    }
}
