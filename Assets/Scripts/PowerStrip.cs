using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerStrip : MonoBehaviour
{
    [SerializeField]
    public Starter Starter;
    private List<GameObject> childChevrons;
    void Start()
    {
        childChevrons = new List<GameObject>();
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            childChevrons.Add(child);
        }
        Debug.Log(childChevrons.Count);
    }

    // Update is called once per frame
    void Update()
    {
        if (Starter.power == 0)
        {
            foreach (GameObject child in childChevrons)
                ChangeChildGlow(child, false);
        }
        float ratio = Starter.power / Starter.maxPower;
        int numberOfLights = Mathf.CeilToInt(ratio * childChevrons.Count);
        for (int i = 0; i < numberOfLights; i++)
        {
            ChangeChildGlow(childChevrons[i], true);

        }
        void ChangeChildGlow(GameObject child, bool glow)
        {
            Renderer renderer = child.GetComponent<Renderer>();
            Material material = renderer.material;
            if (glow)
                material.EnableKeyword("_EMISSION");
            else
                material.DisableKeyword("_EMISSION");
        }
    }
}