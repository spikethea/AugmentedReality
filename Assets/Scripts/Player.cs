using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using Color = UnityEngine.Color;

public class Player : MonoBehaviour
{
    public HeadUI headUI;
    public RightHandInput cameraRig;
    public float fadeSpeed = 0.5f;

    private float damageTimeout = 0;
    private int playerHealth = 3;

    public Texture DeathTexture;
    private Material mat;
    private Color color;

    

    //coroutine variable
    private Coroutine fadeRoutine;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mat = headUI.filter.material;
        color = mat.color;

        headUI.filter.material.color = new Color(1, 1, 1, 1);
        StartCoroutine(Fade(1f));
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("Taking Damage");
        playerHealth -= damage;

        if (damageTimeout <= 0)
        {
            //headUI.filter.material.color = new UnityEngine.Color(1, 1, 1, (float)playerHealth / 2f);
            if (fadeRoutine != null)
                StopCoroutine(fadeRoutine);

            fadeRoutine = StartCoroutine(Fade(1 /(float)playerHealth));
        }
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
        if (fadeRoutine != null)
            StopCoroutine(fadeRoutine);

        headUI.filter.material.color = new Color(0.5f, 0.5f, 0.5f, 1);
        headUI.filter.material.SetTexture("Material.001", DeathTexture);
        headUI.setHeadCanvasTitle("YOU DIED");
        cameraRig.enabled = false;

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("EnemyAttacking"))
        {
            Debug.Log(other.name);
            if (damageTimeout > 0) return;

            TakeDamage(1);
            Rigidbody enemyRb = other.attachedRigidbody;
            //if (enemyRb != null)
            //{
            //    Vector3 pushDir = (other.transform.position - transform.position).normalized;
            //    float pushForce = 2f;

            //    enemyRb.AddForce(pushDir * pushForce, ForceMode.Impulse);
            //}
            damageTimeout = 5;
        }
    }

    IEnumerator Fade(float startAlpha)
    {
        color = mat.color;
        color.a = startAlpha;
        mat.color = color;

        while (color.a > 0)
        {
            color.a -= fadeSpeed * Time.deltaTime;
            mat.color = color;
            yield return null;
        }
    }


    //private void FadeOut()
    //{
    //    color = mat.color;

    //    if (color.a > 0)
    //    {
    //        color.a -= fadeSpeed * Time.deltaTime;
    //        color.a = Mathf.Clamp01(color.a); // keep between 0?
    //        mat.color = color;
    //    }
    //}

    // Update is called once per frame
    void Update()
    {
        if (damageTimeout > 0)
        {
            damageTimeout -= Time.deltaTime;
        }

    }
}
