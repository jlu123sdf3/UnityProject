using UnityEngine;

public class Jumper : MonoBehaviour
{
    public float jumpForce = 0.001f; // Сила прыжка
    public static int score = 0; // Статическая переменная для счёта

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Rigidbody ballRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            if (ballRigidbody != null)
            {
                // Добавляем силу прыжка
                ballRigidbody.AddForce(Vector3.forward * jumpForce);

                // Увеличиваем счёт
                score++;
                Debug.Log("Score: " + score); // Выводим счёт в консоль
            }
        }
    }
}