using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP : MonoBehaviour
{
    [SerializeField]
    private GameObject sibling;
    private TP siblingScript;
    private bool enabledTeleport = true;
    private void Start()
    {
        siblingScript =
            (TP)sibling.GetComponent(typeof(TP));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (enabledTeleport && other.gameObject.CompareTag("Ball"))
        {
            Debug.Log("ball");
            siblingScript.DisableTeleporter();
            // Don't just swap positions - higher y value can make the ball bounce out of the board
            other.transform.position = new Vector3(sibling.transform.position.x, other.transform.position.y, sibling.transform.position.z);
            other.attachedRigidbody.velocity = new Vector3(-other.attachedRigidbody.velocity.x, other.attachedRigidbody.velocity.y, -other.attachedRigidbody.velocity.z);
        }
    }
    public void DisableTeleporter()
    {
        StartCoroutine(StartDisableTeleporter());
    }
    IEnumerator StartDisableTeleporter()
    {
        enabledTeleport = false;
        yield return new WaitForSeconds(0.5f);
        enabledTeleport = true;
    }
}


