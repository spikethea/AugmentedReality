using UnityEngine;

public class Smashable : MonoBehaviour
{
    public GameObject SolidMesh;
    public GameObject BrokenMesh;

    public AudioClip smashSound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    public void Collapse()
    {
        SolidMesh.SetActive(false);
        BrokenMesh.SetActive(true);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("ChewingGum"))
        {
            SolidMesh.SetActive(false);
            BrokenMesh.SetActive(true);

        }
    }
}
