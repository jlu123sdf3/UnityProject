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
            other.transform.position = sibling.transform.position;
            siblingScript.DisableTeleporter();
            DisableTeleporter();
        }
    }
    public void DisableTeleporter()
    {
        StartCoroutine(startDisableTeleporter());
    }
    IEnumerator startDisableTeleporter()
    {
        Renderer renderer = GetComponent<Renderer>();
        Material material = renderer.material;

        material.DisableKeyword("_EMISSION");
        enabledTeleport = false;

        yield return new WaitForSeconds(5);

        material.EnableKeyword("_EMISSION");
        enabledTeleport = true;
    }
}
