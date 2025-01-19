using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
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
    private Rigidbody ball;

    // Flippers control keys
    [SerializeField] private KeyCode[] leftKeys = { KeyCode.LeftControl };
    [SerializeField] private KeyCode[] rightKeys = { KeyCode.RightControl };

    [Header("Table Hit Settings")]
    [Tooltip("Force applied when hitting the table.")]
    [Range(0f, 0.05f)]
    [SerializeField] private float hitForce = 0.01f;

    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform planeTransform;
    private Vector3 originalCameraPosition;
    private Vector3 originalPlanePosition;
    [SerializeField] private float shakeIntensity = 0.1f;
    [SerializeField] private float shakeDuration = 0.05f;

    [SerializeField] private AudioClip shakeSound;

    public bool originalBall = true;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        ball = GetComponent<Rigidbody>();
        if (cameraTransform != null)
        {
            originalCameraPosition = cameraTransform.localPosition;
        }
        if (planeTransform != null)
        {
            originalPlanePosition = planeTransform.localPosition;
        }
    }

    private bool IsAnyKeyDownPressed(KeyCode[] keys)
    {
        foreach (KeyCode key in keys)
        {
            if (Input.GetKeyDown(key))
            {
                return true;
            }
        }
        return false;
    }

    private void ShakeCamera(Vector3 direction)
    {
        if (cameraTransform == null) return;

        audioSource.PlayOneShot(shakeSound);

        float offsetX = UnityEngine.Random.Range(0, shakeIntensity) * direction.x;
        float offsetY = UnityEngine.Random.Range(-shakeIntensity, 0);

        UnityEngine.Debug.Log("Shake: " + offsetX);

        cameraTransform.localPosition = originalCameraPosition + new Vector3(offsetX, 0f, offsetY);
        planeTransform.localPosition = originalPlanePosition + new Vector3(offsetX, 0f, offsetY);

        Invoke(nameof(ResetCameraPosition), shakeDuration);
    }

    private void ResetCameraPosition()
    {
        if (cameraTransform != null)
        {
            cameraTransform.localPosition = originalCameraPosition;
        }
        if (planeTransform != null)
        {
            planeTransform.localPosition = originalPlanePosition;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (IsAnyKeyDownPressed(leftKeys))
        {
            ball.AddForce(new Vector3(1f, 0f, 1f) * hitForce, ForceMode.Impulse);
            ShakeCamera(Vector3.left);
        }
        if (IsAnyKeyDownPressed(rightKeys))
        {
            ball.AddForce(new Vector3(-1f, 0f, 1f) * hitForce, ForceMode.Impulse);
            ShakeCamera(Vector3.right);
        }
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
