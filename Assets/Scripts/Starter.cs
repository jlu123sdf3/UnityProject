using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Starter : MonoBehaviour
{
    float power;
    float minPower = 0f;
    [SerializeField] float maxPower = 500;  // Увеличили силу для более заметного движения
    [SerializeField] float powerStep = 100;  // Увеличили шаг силы для более быстрого нарастания
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
                // Добавляем увеличенную силу по оси Z
                ball.AddForce(power * Vector3.forward, ForceMode.Impulse);  // Используем Impulse для мгновенной силы
                power = minPower; // Сбрасываем силу после применения
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