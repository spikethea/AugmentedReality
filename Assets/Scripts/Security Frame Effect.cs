using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class SecurityFrameEffect : MonoBehaviour
{
    // Security Screws
    public GameObject Screw1;
    public GameObject Screw2;
    public GameObject Screw3;
    public GameObject Screw4;

    // Frame
    public ParticleSystem FogEffect;
    public GameObject MetalFrame;
    public GameObject SolidPicture;
    public UnityEvent SmashedEvent;

    // Enemy Spawner
    public GameObject spawner;

    private bool FrameUnlocked = false;

    public AudioClip breakSound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (SmashedEvent == null)
            SmashedEvent = new UnityEvent();
    }

    private void Awake()
    {
        FogEffect.Stop();
        MetalFrame.GetComponent<Rigidbody>().useGravity = false;
    }

    private bool screwsAreDestroyed()
    {
        if (
            Screw1.IsDestroyed() &&
            Screw2.IsDestroyed() &&
            Screw3.IsDestroyed() &&
            Screw4.IsDestroyed()
            ) {
            return true;
            } else return false;
    }


    void Update()
    {
        if (screwsAreDestroyed() && FrameUnlocked) {
            SolidPicture.SetActive(false);
            MetalFrame.GetComponent<Rigidbody>().useGravity = true;
            Destroy(MetalFrame, 5f);
            SmashedEvent.Invoke();
            FogEffect.Play();
            FrameUnlocked = true;
        }
    }
}
