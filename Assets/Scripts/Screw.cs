using UnityEngine;

public class Screw : MonoBehaviour
{
    // Screw Properties
    private Renderer rend;


    // Red Oscillating
    private float speed = 1f;
    private Color startColor;

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    public void Erode()
    {
        rend = GetComponent<Renderer>();
        Destroy(transform, 300);
    }

    void Update()
    {
        float t = (Mathf.Sin(Time.time * speed) + 1f) / 2f;
        rend.material.color = Color.Lerp(startColor, Color.red, t);
    }
}