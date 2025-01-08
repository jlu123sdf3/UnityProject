using UnityEngine;

public class Jumper : MonoBehaviour
{
    public float jumpForce = 0.001f; // ���� ������
    public static int score = 0; // ����������� ���������� ��� �����

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Rigidbody ballRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            if (ballRigidbody != null)
            {
                // ��������� ���� ������
                ballRigidbody.AddForce(Vector3.forward * jumpForce);

                // ����������� ����
                score++;
                Debug.Log("Score: " + score); // ������� ���� � �������
            }
        }
    }
}