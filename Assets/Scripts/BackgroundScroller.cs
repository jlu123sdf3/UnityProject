using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public float scrollSpeed = 0.5f; 
    private Vector2 offset;
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        offset = new Vector2(Time.time * scrollSpeed, 0); 
        rend.material.mainTextureOffset = offset;
    }
}