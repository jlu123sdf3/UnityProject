using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Flipper : MonoBehaviour
{
    [Header("Flipper Hit Settings Modifier")]
    [Tooltip("Force multiplier applied by the flipper when hitting the ball.")]
    [Range(0.1f, 10f)]
    [SerializeField] float hitStrenght = 5f;

    [Header("Ball Speed Limiter")]
    [Tooltip("Maximum speed that the ball can reach after being hit by the flipper.")]
    [Range(10f, 500f)]
    [SerializeField] float maxSpeed = 100f;

    [Header("Flipper Curve Modifiers")]
    [Tooltip("Curve adjustment for the left flipper's ball trajectory. Higher values curve the ball more.")]
    [Range(0f, 10f)]
    [SerializeField] float curveModifierLeft = 0.5f;
    [Tooltip("Curve adjustment for the right flipper's ball trajectory. Higher values curve the ball more.")]
    [Range(0f, 10f)]
    [SerializeField] float curveModifierRight = 0.5f;

    [Tooltip("Indicates whether this flipper is the left flipper. (default - right flipper)")]
    [SerializeField] private bool leftFlipper;

    float restPos = 0f;
    float pressedPos;
    private HingeJoint hinge;
    private JointSpring spring;

    // Flippers control keys
    [SerializeField] private KeyCode[] leftKeys = { KeyCode.A, KeyCode.LeftArrow, KeyCode.LeftShift };
    [SerializeField] private KeyCode[] rightKeys = { KeyCode.D, KeyCode.RightArrow, KeyCode.RightShift };

    void Start()
    {
        hinge = GetComponent<HingeJoint>();
        spring = hinge.spring;
        pressedPos = spring.targetPosition;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Rigidbody ballRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            if (ballRigidbody != null)
            {
                Vector3 ballVelocity = ballRigidbody.velocity;
                Vector3 collisionNormal = collision.contacts[0].normal;
                // Arcade
                if (leftFlipper)
                {
                    collisionNormal = Vector3.Lerp(collisionNormal, transform.right, curveModifierLeft);
                }
                else
                {
                    collisionNormal = Vector3.Lerp(collisionNormal, -transform.right, curveModifierRight);
                }

                Vector3 reflectedVelocity = Vector3.Reflect(ballVelocity, collisionNormal);

                Vector3 flipperVelocity = Vector3.zero;
                Rigidbody flipperRigidbody = GetComponent<Rigidbody>();
                if (flipperRigidbody != null)
                {
                    flipperVelocity = flipperRigidbody.velocity;
                }

                Vector3 finalVelocity = reflectedVelocity + flipperVelocity * hitStrenght;
                // Arcade
                finalVelocity = Vector3.ClampMagnitude(finalVelocity, maxSpeed);


                ballRigidbody.AddForce(finalVelocity - ballRigidbody.velocity, ForceMode.VelocityChange);
            }
        }
    }

    private bool IsAnyKeyPressed(KeyCode[] keys)
    {
        foreach (KeyCode key in keys)
        {
            if (Input.GetKey(key))
            {
                return true;
            }
        }
        return false;
    }

    void Update()
    {
        if (leftFlipper && IsAnyKeyPressed(leftKeys))
        {
            spring.targetPosition = pressedPos;
        }
        else if (!leftFlipper && IsAnyKeyPressed(rightKeys))
        {
            spring.targetPosition = pressedPos;
        }
        else
        {
            spring.targetPosition = restPos;
        }

        hinge.spring = spring;
    }
}