using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Starter : MonoBehaviour
{
    float power;
    float minPower = 0f;
    [SerializeField] float maxPower = 500;  // ��������� ���� ��� ����� ��������� ��������
    [SerializeField] float powerStep = 100;  // ��������� ��� ���� ��� ����� �������� ����������
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
                // ��������� ����������� ���� �� ��� Z
                ball.AddForce(power * Vector3.forward, ForceMode.Impulse);  // ���������� Impulse ��� ���������� ����
                power = minPower; // ���������� ���� ����� ����������
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