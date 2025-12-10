using UnityEngine;
using TMPro;

public class HeadUI : MonoBehaviour
{
    public Canvas HeadUICanvas;
    public MeshRenderer filter;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void hideHeadCanvas() {
        if (HeadUICanvas.enabled)
        {
            HeadUICanvas.enabled = false; 
        }
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
