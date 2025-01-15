using UnityEngine;

public class Jumper : MonoBehaviour
{
    private float jumpForce; // Jump power.
    private int addedPoints; // Jump power.
    PlayerPointsObject points;
    [SerializeField] bool extraJumper = false;

    [Header("Ball Collision Sound Settings")]
    [Tooltip("The sound played when the ball hits something.")]
    [SerializeField] private AudioClip hitSound;

    private AudioSource audioSource;

    private void Awake()
    {
        jumpForce = extraJumper ? 0.01f : 0.015f;
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

                // Adding Jump Force
                ballRigidbody.AddForce(Vector3.up * jumpForce);

                // Increasing the score
                points.Score+= addedPoints;
            }
        }
    }
}