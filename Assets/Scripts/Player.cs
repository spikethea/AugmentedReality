using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using Color = UnityEngine.Color;

public class Player : MonoBehaviour
{
    public HeadUI headUI;
    public float fadeSpeed = 0.5f;

    private float damgeTimeout = 0;
    private int playerHealth = 3;

    private Material mat;
    private Color color;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mat = headUI.filter.material;
        color = mat.color;

        headUI.filter.material.color = new Color(1, 1, 1, 1);
    }

    public void TakeDamage(int damage)
    {
        playerHealth -= damage;
        headUI.filter.material.color = new UnityEngine.Color(1, 1, 1, playerHealth/3);
        if (playerHealth <= 0)
        {
            Debug.Log("Player has died.");
            KillPlayer();
        }
    }

    private void KillPlayer()
    {
        // Handle player death (e.g., play animation, restart level, etc.)
        Debug.Log("Handling player death...");
        headUI.filter.material.color = new Color(0.2f, 0.2f, 0.2f, 1);

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (damgeTimeout > 0) return;
            TakeDamage(1);
            damgeTimeout = 5;
        }
    }

    private void FadeOut()
    {


        if (color.a > 0)
        {
            color.a -= fadeSpeed * Time.deltaTime;
            color.a = Mathf.Clamp01(color.a); // keep between 0–1
            mat.color = color;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (damgeTimeout > 0)
        {
            damgeTimeout -= Time.deltaTime;
        }

        if (mat.color.a > 0 && playerHealth >= 0) {
            FadeOut();
        }

    }
}
