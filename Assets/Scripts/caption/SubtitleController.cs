using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class SubtitleController : MonoBehaviour
{
    [System.Serializable]
    public class SubtitleLine
    {
        public string text;
        public float displayTime = 3f; // 显示持续时间
        public float fadeInDuration = 0.5f; // 淡入时间
        public float fadeOutDuration = 0.5f; // 淡出时间
    }

    [Header("字幕设置")]
    public TextMeshProUGUI subtitleText;
    public List<SubtitleLine> subtitles = new List<SubtitleLine>();

    [Header("动画设置")]
    public float delayBetweenSubtitles = 0.2f; // 字幕间隔
    public bool autoPlay = true;
    public bool loop = false;

    private int currentSubtitleIndex = 0;
    private Coroutine subtitleCoroutine;

    void Start()
    {
        if (subtitleText != null)
        {
            // 初始化时设置为透明
            Color color = subtitleText.color;
            color.a = 0;
            subtitleText.color = color;
            subtitleText.text = "";
        }

        if (autoPlay)
        {
            PlaySubtitles();
        }
    }

    public void PlaySubtitles()
    {
        if (subtitleCoroutine != null)
        {
            StopCoroutine(subtitleCoroutine);
        }
        currentSubtitleIndex = 0;
        subtitleCoroutine = StartCoroutine(SubtitleSequence());
    }

    public void StopSubtitles()
    {
        if (subtitleCoroutine != null)
        {
            StopCoroutine(subtitleCoroutine);
            subtitleCoroutine = null;
        }

        // 重置字幕显示
        if (subtitleText != null)
        {
            Color color = subtitleText.color;
            color.a = 0;
            subtitleText.color = color;
            subtitleText.text = "";
        }
    }

    private IEnumerator SubtitleSequence()
    {
        do
        {
            for (currentSubtitleIndex = 0; currentSubtitleIndex < subtitles.Count; currentSubtitleIndex++)
            {
                SubtitleLine subtitle = subtitles[currentSubtitleIndex];

                // 显示当前字幕
                yield return StartCoroutine(ShowSubtitle(subtitle));

                // 字幕间隔
                if (currentSubtitleIndex < subtitles.Count - 1)
                {
                    yield return new WaitForSeconds(delayBetweenSubtitles);
                }
            }
        } while (loop);

        subtitleCoroutine = null;
    }

    private IEnumerator ShowSubtitle(SubtitleLine subtitle)
    {
        if (subtitleText == null) yield break;

        // 设置文本
        subtitleText.text = subtitle.text;

        // 淡入效果
        yield return StartCoroutine(FadeText(0, 1, subtitle.fadeInDuration));

        // 保持显示
        yield return new WaitForSeconds(subtitle.displayTime);

        // 淡出效果
        yield return StartCoroutine(FadeText(1, 0, subtitle.fadeOutDuration));

        // 清空文本
        subtitleText.text = "";
    }

    private IEnumerator FadeText(float startAlpha, float endAlpha, float duration)
    {
        float elapsedTime = 0;
        Color color = subtitleText.color;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            color.a = alpha;
            subtitleText.color = color;
            yield return null;
        }

        // 确保最终值准确
        color.a = endAlpha;
        subtitleText.color = color;
    }

    // 手动触发下一条字幕
    public void ShowNextSubtitle()
    {
        if (currentSubtitleIndex < subtitles.Count)
        {
            StartCoroutine(ShowSubtitle(subtitles[currentSubtitleIndex]));
            currentSubtitleIndex++;
        }
    }
}