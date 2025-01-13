using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Starter : MonoBehaviour
{
    public float power = 0;
    public float minPower = 0f;
    public float maxPower = 5;  // Increased strength for more noticeable movement
    float powerStep = 3;  // Increased force step for faster ramp-up
    [SerializeField] UnityEngine.UI.Slider powerSlider;
    private Rigidbody ball = null;
    bool ballReady;

    void Start()
    {
        powerSlider.minValue = minPower;
        powerSlider.maxValue = maxPower;
    }

    void Update()
    {
        if (ball != null)
        {
            ballReady = true;
            if (Input.GetKey(KeyCode.Space))
            {
                if (power <= maxPower)
                {
                    power += powerStep * Time.deltaTime;
                }
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                // Add increased force along the Z axis
                ball.AddForce(power * Vector3.forward, ForceMode.Impulse);  // Using Impulse for instant power
                power = minPower; // Reset the power after use
            }
        }
        else
        {
            ballReady = false;
            power = minPower;
        }

        powerSlider.gameObject.SetActive(ballReady);
        powerSlider.value = power;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            ball = other.gameObject.GetComponent<Rigidbody>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            ball = null;
            power = minPower;
        }
    }
}