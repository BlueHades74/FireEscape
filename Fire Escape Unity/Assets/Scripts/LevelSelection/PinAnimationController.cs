using UnityEngine;
using System.Collections;
public class PinAnimationController : MonoBehaviour
{
    [Header("Animation Settings")]
    public float dropHeight = 100f;
    public float dropDuration = 0.3f;
    public float bounceHeight = 15f;
    public float bounceDuration = 0.15f;
    public float delay = 0f;

    private RectTransform rectTransform;
    private Vector2 originalPosition;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.anchoredPosition;
    }

    private void OnEnable()
    {
        rectTransform.anchoredPosition = originalPosition + Vector2.up * dropHeight;
        StartCoroutine(DropAnimation());
    }

    private IEnumerator DropAnimation()
    {
        yield return new WaitForSeconds(delay);

        float t = 0f;
        while (t < dropDuration)
        {
            t += Time.deltaTime;
            float normalized = t / dropDuration;
            float eased = 1f - Mathf.Pow(1f - normalized, 3f); // Ease-out cubic
            rectTransform.anchoredPosition = originalPosition + Vector2.up * (dropHeight * (1f - eased));
            yield return null;
        }

        t = 0f;
        Vector2 downPos = originalPosition;
        Vector2 upPos = originalPosition + Vector2.up * bounceHeight;

        while (t < bounceDuration)
        {
            t += Time.deltaTime;
            float normalized = t / bounceDuration;
            rectTransform.anchoredPosition = Vector2.Lerp(downPos, upPos, Mathf.Sin(normalized * Mathf.PI));
            yield return null;
        }

        rectTransform.anchoredPosition = originalPosition;
    }
}
