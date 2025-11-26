using UnityEngine;

public class Smashable : MonoBehaviour
{
    public GameObject SolidMesh;
    public GameObject BrokenMesh;
    // Smashing time thresho0ld
    [SerializeField] [Range (0, 5)] private float smashTimeThreshold = 2f;
    private float smashTimer = 0.0f;
    private bool isCollapsed = false;

    public AudioClip smashSound;
    public AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    public void TimerCollapse()
    {
        smashTimer += Time.deltaTime;
        Debug.Log("Smash Timer: " + smashTimer);
         audioSource = GetComponent<AudioSource>();
        if (smashTimer >= smashTimeThreshold)
            Collapse();
    }

    public void Collapse()
    {
        SolidMesh.SetActive(false);
        BrokenMesh.SetActive(true);
        if (!audioSource.isPlaying && !isCollapsed)
        {
            audioSource.PlayOneShot(smashSound, 1);
            isCollapsed = true;
        }
    }



    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("ChewingGum"))
        {
            Collapse();
        }
    }
}
