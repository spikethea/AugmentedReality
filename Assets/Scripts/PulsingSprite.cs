using System.Collections;
using UnityEngine;

public class PulsingSprite : MonoBehaviour
{
    [Range(0, 5)] public float PulseLength = 2f;
    [Range(0, 1)] public float PulseAmount = 0.0f;
    public SpriteRenderer spriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Color tmp = spriteRenderer.material.color;


        //StartCoroutine(SpriteFade(tmp, PulseAmount, PulseLength));

        float alpha = (Mathf.Sin(Time.time * PulseLength) + 1f) / 2f; // oscillates between 0 and 1
        Color color = spriteRenderer.color;
        color.a = alpha;
        spriteRenderer.color = color;

        transform.LookAt(Camera.main.transform.position, -Vector3.up);

    }

    public void StartFade()
    {
        Color tmp = spriteRenderer.material.color;
        StartCoroutine(SpriteFadeCoroutine(tmp, PulseAmount, PulseLength));
    }

    public IEnumerator SpriteFadeCoroutine(
            Color spriteColor,
            float endValue,
            float duration)
    {
        float elapsedTime = 0;
        float startValue = spriteColor.a;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, endValue, elapsedTime / duration);
            spriteColor = new Color(spriteColor.r, spriteColor.g, spriteColor.b, newAlpha);
            updateSprite(spriteColor.a);
            Debug.Log("EndValue: " + endValue);
            Debug.Log("Alpha: " + spriteColor.a);
            yield return null;
        }
        startValue = endValue;
        Debug.Log("While Loop Finished");
        //PulseAmount = 1 - endValue;
        yield return null;
    }

    void updateSprite(float alpha)
    {
        Color tmp = spriteRenderer.material.color;
        tmp.a = alpha;
        spriteRenderer.material.color = tmp;
    }
}
