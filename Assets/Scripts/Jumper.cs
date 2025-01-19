using System.Diagnostics;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    private float jumpForce; // Jump power.
    private int addedPoints; // Jump power.
    PlayerPointsObject points;
    [SerializeField] bool extraJumper = false;

    [Header("Ball Collision Power Settings")]
    [SerializeField] private float basePower = 1.2f;
    [SerializeField] private float extraPower = 1.6f;

    [Header("Ball Collision Sound Settings")]
    [Tooltip("The sound played when the ball hits something.")]
    [SerializeField] private AudioClip hitSound;

    [SerializeField] float maxSpeed = 100f;
    [SerializeField] float hopUp = 1.0f;

    private AudioSource audioSource;

    private void Awake()
    {
        jumpForce = extraJumper ? basePower : extraPower;
        addedPoints = extraJumper ? 1 : 2;
        points = FindObjectOfType<PlayerPointsObject>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Rigidbody ballRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            if (ballRigidbody != null)
            {
                audioSource.PlayOneShot(hitSound);

                Vector3 ballVelocity = ballRigidbody.velocity;
                Vector3 collisionNormal = collision.contacts[0].normal;

                collisionNormal = Vector3.Lerp(collisionNormal, transform.forward, hopUp);

                Vector3 reflectedVelocity = Vector3.Reflect(ballVelocity, collisionNormal);

                Vector3 finalVelocity = reflectedVelocity * jumpForce;

                finalVelocity = Vector3.ClampMagnitude(finalVelocity, maxSpeed);

                ballRigidbody.AddForce(finalVelocity - ballRigidbody.velocity, ForceMode.VelocityChange);

                // Increasing the score
                points.Score+= addedPoints;
            }
        }
    }
}