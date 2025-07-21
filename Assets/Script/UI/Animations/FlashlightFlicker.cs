using UnityEngine;
using System.Collections;

public class FlashlightFlicker : MonoBehaviour
{
    [Header("Flicker Settings")]
    public float minFlickerDelay = 0.5f;
    public float maxFlickerDelay = 2f;
    public float minOffTime = 0.2f;
    public float maxOffTime = 0.4f;
    public float fadeDuration = 0.1f;

    [Header("Materials to Flicker")]
    public Renderer[] renderersToFlicker;

    private Material[] flickerMaterials;
    private Color[] originalColors;

    void Start()
    {
        // Duplicate materials to avoid editing shared materials
        flickerMaterials = new Material[renderersToFlicker.Length];
        originalColors = new Color[renderersToFlicker.Length];

        for (int i = 0; i < renderersToFlicker.Length; i++)
        {
            flickerMaterials[i] = renderersToFlicker[i].material; // Creates an instance
            originalColors[i] = flickerMaterials[i].color;
        }

        StartCoroutine(FlickerRoutine());
    }

    IEnumerator FlickerRoutine()
    {
        while (true)
        {
            float delay = Random.Range(minFlickerDelay, maxFlickerDelay);
            yield return new WaitForSeconds(delay);

            // Fade out
            yield return StartCoroutine(FadeToAlpha(0f));

            float offTime = Random.Range(minOffTime, maxOffTime);
            yield return new WaitForSeconds(offTime);

            // Fade back in
            yield return StartCoroutine(FadeToAlpha(1f));
        }
    }

    IEnumerator FadeToAlpha(float targetAlpha)
    {
        float t = 0f;

        Color[] startColors = new Color[flickerMaterials.Length];
        for (int i = 0; i < flickerMaterials.Length; i++)
            startColors[i] = flickerMaterials[i].color;

        while (t < fadeDuration)
        {
            float normalizedTime = t / fadeDuration;
            for (int i = 0; i < flickerMaterials.Length; i++)
            {
                Color start = startColors[i];
                Color end = new Color(start.r, start.g, start.b, originalColors[i].a * targetAlpha);
                flickerMaterials[i].color = Color.Lerp(start, end, normalizedTime);
            }
            t += Time.deltaTime;
            yield return null;
        }

        // Ensure final alpha is exact
        for (int i = 0; i < flickerMaterials.Length; i++)
        {
            Color finalColor = flickerMaterials[i].color;
            finalColor.a = originalColors[i].a * targetAlpha;
            flickerMaterials[i].color = finalColor;
        }
    }
}
