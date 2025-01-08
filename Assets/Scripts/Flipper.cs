using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flipper : MonoBehaviour
{
    [SerializeField] float restPos = 0f;
    [SerializeField] float pressedPos = 45f;
    [SerializeField] float hitStrenght = 10f;
    [SerializeField] float flipperDamper = 10f;
    [SerializeField] private bool leftFlipper;
    private HingeJoint hinge;
    private JointSpring spring;


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
                // Additional impulse in the direction of movement of the flipper

                Vector3 flipperForce = transform.right * hitStrenght * 0.001f; // Direction and strength of impulse
                ballRigidbody.AddForce(flipperForce, ForceMode.Impulse);
            }
            Debug.Log("Flipper hit the ball with velocity: " + collision.relativeVelocity.magnitude);
        }
    }


    void Update()
    {
        spring.spring = hitStrenght;
        spring.damper = flipperDamper;


        if (leftFlipper && Input.GetAxis("Horizontal") < 0)
        {
            spring.targetPosition = pressedPos;
        }
        else if (!leftFlipper && Input.GetAxis("Horizontal") > 0)
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