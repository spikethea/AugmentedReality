using UnityEngine;

public class BrokenGlassEffect : MonoBehaviour
{
    public GameObject SolidGlass;
    public GameObject BrokenGlass;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.relativeVelocity.magnitude > 0.2)
        {
            SolidGlass.SetActive(false);
            BrokenGlass.SetActive(true);
        }
    }
}
