using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeToBlack : MonoBehaviour
{
    public float fadeDuration = 2.0f; // Duration of the fade in seconds
    private Image fadeImage;
    private CanvasGroup canvasGroup;

    private void Start()
    {
        fadeImage = GetComponent<Image>();
        canvasGroup = GetComponent<CanvasGroup>();
        StartCoroutine(FadeOutAndLoadNextScene());
    }

    private IEnumerator FadeOutAndLoadNextScene()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            canvasGroup.alpha = alpha;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Here, you can load the next scene or trigger other actions.
    }
}
