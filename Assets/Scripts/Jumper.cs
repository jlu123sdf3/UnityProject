using UnityEngine;

public class Jumper : MonoBehaviour
{
    float jumpForce = 0.001f; // Jump power.
  
    public static int score = 0; // Static variable for counting

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
                score++;
                Debug.Log("Score: " + score); // We output the score to the console
            }
        }
    }
}