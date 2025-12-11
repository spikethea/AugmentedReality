using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using Color = UnityEngine.Color;

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

    private Material mat;
    private Color color;
    public float fadeSpeed = 0.5f;

    // Enemy Spawner
    public EnemySpawner spawner;

    private bool FrameUnlocked = false;

    public AudioClip breakSound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (SmashedEvent == null)
            SmashedEvent = new UnityEvent();

        mat = SolidPicture.GetComponent<MeshRenderer>().material;
        color = mat.color;
        Debug.Log(color);
    }

    private void Awake()
    {
        FogEffect.Stop();
        MetalFrame.GetComponent<Rigidbody>().useGravity = false;
        MetalFrame.GetComponent<BoxCollider>().enabled = false;
    }

    private void PlaySound() {
        AudioSource audioSource = GetComponent<AudioSource>();
        if (!audioSource.isPlaying && audioSource)
        {
            audioSource.PlayOneShot(breakSound, 1);
        }

        MetalFrame.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1, 0.5f);
    }

    private bool screwsAreDestroyed()
    {
        if (
            !Screw1.activeSelf &&
            !Screw2.activeSelf &&
            !Screw3.activeSelf &&
            !Screw4.activeSelf
            ) {
            PlaySound();
            return true;
            } else return false;
    }


    IEnumerator Fade(float startAlpha)
    {
        Debug.Log("Start Fade");
        color = mat.color;
        color.a = startAlpha;
        mat.color = color;

        while (color.a > 0)
        {
            Debug.Log("Fading");
            color.a -= fadeSpeed * Time.deltaTime;
            mat.color = color;
            yield return null;
        }
    }

    void Update()
    {

        if (screwsAreDestroyed() && !FrameUnlocked)
        {
            Debug.Log("spawner.IsSpawnerStarted: " + spawner.IsSpawnerStarted);
           
            MetalFrame.GetComponent<Rigidbody>().useGravity = true;
            MetalFrame.GetComponent<BoxCollider>().enabled = true;
            
            Destroy(SolidPicture, 5f);
            Destroy(MetalFrame, 5f);
            SmashedEvent.Invoke();
            FogEffect.Play();
            spawner.IsSpawnerStarted = true;
            FrameUnlocked = true;
            
            StartCoroutine(Fade(1f));
        }

        
    }

}
