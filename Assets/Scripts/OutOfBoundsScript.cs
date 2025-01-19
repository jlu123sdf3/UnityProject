using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OutOfBoundsScript : MonoBehaviour
{
    PlayerLivesObject lives;
    public GameObject ball;
    Vector3 startingPosition;
    // Start is called before the first frame update
    void Start()
    {
        lives = FindObjectOfType<PlayerLivesObject>();
        ball = GameObject.FindGameObjectWithTag("Ball");
        startingPosition = ball.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Rigidbody ballRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            if (ballRigidbody != null)
            {
                if (lives.Lives > 1) { 
                    lives.Lives--; 
                    ball.transform.position = startingPosition;
                    // Stopping the ball
                    ballRigidbody.velocity = Vector3.zero;
                    ballRigidbody.angularVelocity = Vector3.zero;
                }
                else
                {
                    SaveHighScore();
                    Destroy(ball);

                    SceneManager.LoadScene("GameOverScene", LoadSceneMode.Additive);
                }
            }
        }
    }

    private void SaveHighScore()
    {
        PlayerPointsObject pointsObject = FindObjectOfType<PlayerPointsObject>();
        if (pointsObject != null)
        {
            int finalScore = pointsObject.Score;
            string playerName = PlayerPrefs.GetString("nametag", "Player");
            HighScore.HighScoreManager.AddHighScore(playerName, finalScore); 
            UnityEngine.Debug.Log($"Saved: {playerName} - {finalScore}");
        }
        else
        {
            UnityEngine.Debug.LogWarning("No PlayerPointsObject");
        }
    }
}
