using UnityEngine;

public class Jumper : MonoBehaviour
{
    private float jumpForce; // Jump power.
    private int addedPoints; // Jump power.
    PlayerPointsObject points;
    [SerializeField]
    bool extraJumper = false;
    private void Awake()
    {
        jumpForce = extraJumper ? 0.001f : 0.0015f;
        addedPoints = extraJumper ? 1 : 2;
        points = FindObjectOfType<PlayerPointsObject>();
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Rigidbody ballRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            if (ballRigidbody != null)
            {
                // Adding Jump Force
                ballRigidbody.AddForce(Vector3.forward * jumpForce);

                // Increasing the score
                points.Score+= addedPoints;
            }
        }
    }
}