using UnityEngine;

public class Screw : MonoBehaviour
{
    // Screw Properties
    private Renderer rend;
    public int erodeThreshold = 100;
    
    private int erodeTimer = 0;

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
        Debug.Log("Erode" + gameObject.name);
        erodeTimer++;
        if (erodeThreshold < erodeTimer) {
            gameObject.SetActive(false);
        }
        
    }

    void Update()
    {
        float t = (Mathf.Sin(Time.time * speed) + 1f) / 2f;
        rend.material.color = Color.Lerp(startColor, Color.red, t);
    }
}