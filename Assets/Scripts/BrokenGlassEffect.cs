using UnityEngine;
using UnityEngine.Events;

public class BrokenGlassEffect : MonoBehaviour
{
    public GameObject SolidGlass;
    public GameObject BrokenGlass;
    public GameObject stencillWorld;
    public ParticleSystem FogEffect;
    public UnityEvent SmashedEvent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (SmashedEvent == null)
            SmashedEvent = new UnityEvent();
    }

    private void Awake()
    {
        FogEffect.Stop();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("ChewingGum"))
        {
            SolidGlass.SetActive(false);
            BrokenGlass.SetActive(true);
            Destroy(BrokenGlass, 2.5f);
            SmashedEvent.Invoke();
            FogEffect.Play();

        }
    }
}
