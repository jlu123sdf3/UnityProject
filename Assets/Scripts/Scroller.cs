using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Scroller : MonoBehaviour
{
    float scrollHorizontal = 0.003f;
    float scrollVertical = 0.003f;
    Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        rend.material.SetTextureOffset("_MainTex", new Vector2(Time.time * scrollHorizontal, Time.time * scrollVertical));
    }

}
