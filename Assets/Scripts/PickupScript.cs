using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour
{
    private const int rotateAnimationSpeed = 40;
    private const float bounceAnimationSpeed = 0.9f;
    private const float movementBounds = 0.09f;
    private float startingY;

    private IEnumerator Animation()
    {
        while (true) {
            transform.Rotate(Vector3.up, rotateAnimationSpeed * Time.deltaTime,Space.World);
            transform.position = new Vector3(transform.position.x, startingY+(Mathf.Sin(Time.time * bounceAnimationSpeed) * movementBounds), transform.position.z);
            yield return null;
        }
        
    }
    void Start()
    {
        startingY = transform.position.y;
        StartCoroutine(Animation());
    }
}
