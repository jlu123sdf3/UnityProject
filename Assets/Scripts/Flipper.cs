using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Flipper : MonoBehaviour
{
    [SerializeField] float restPos = 0f;
    [SerializeField] float pressedPos = 45f;
    [SerializeField] float springStrenght = 7000f;
    [SerializeField] float hitStrenght = 5f;
    [SerializeField] float flipperDamper = 50f;
    [SerializeField] float maxSpeed = 100f;
    [SerializeField] private bool leftFlipper;
    float curveModifierLeft = 0.5f;
    float curveModifierRight = 0.5f;
    private HingeJoint hinge;
    private JointSpring spring;

    // Flippers control keys
    [SerializeField] private KeyCode[] leftKeys = { KeyCode.A, KeyCode.LeftArrow };
    [SerializeField] private KeyCode[] rightKeys = { KeyCode.D, KeyCode.RightArrow };

    void Start()
    {
        hinge = GetComponent<HingeJoint>();
        hinge.useSpring = true;
        spring = new JointSpring();
        hinge.useLimits = true;
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
                //Arcade
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
                //Arcade
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
        spring.spring = springStrenght;
        spring.damper = flipperDamper;
        hinge.spring = spring;

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
    }
}