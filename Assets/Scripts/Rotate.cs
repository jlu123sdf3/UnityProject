using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    private const float rotationSpeed = 1.5f;
    void Update()
    {
        transform.Rotate(Vector3.right * Time.deltaTime * rotationSpeed);

    }
}
