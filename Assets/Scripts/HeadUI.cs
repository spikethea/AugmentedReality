using UnityEngine;
using TMPro;

public class HeadUI : MonoBehaviour
{
    //UI Head mounted propperties
    public Canvas HeadUICanvas;
    public TextMeshProUGUI Headtitle;
    public TextMeshProUGUI Headcaption;

    //Header Filter
    public MeshRenderer filter;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void hideHeadCanvas() {
        if (HeadUICanvas.enabled)
        {
            HeadUICanvas.enabled = false; 
        }
        
    }

    public void setHeadCanvasTitle(string title)
    {
        if (!HeadUICanvas.enabled)
        {
            Debug.Log("Setting HeadCanvas");
            HeadUICanvas.enabled = true;
            Headtitle.text = title;
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
