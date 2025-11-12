using System.Collections;
using UnityEngine;

public class ButtonAnimatorController : MonoBehaviour
{

    [Header("Animation Timers")]
    public float popDuration = 0.4f;
    public float startScale = 1f;
    public float overshootScale = 1.15f;
    public float settleScale = 1.0f;
    public float delay = 0f;

    private Vector3 originalScale;

    private void OnEnable()
    {
        originalScale = transform.localScale;
        transform.localScale = Vector3.one * startScale;
        StartCoroutine(PopAnimation());
    }

    private IEnumerator PopAnimation()
    {
        yield return new WaitForSeconds(delay);

        float t = 0f;

        while (t < popDuration)
        {
            t += Time.deltaTime;
            float normalized = t / popDuration;

            float scale = Mathf.Lerp(startScale, overshootScale, Mathf.Sin(normalized * Mathf.PI * 0.5f));
            transform.localScale = Vector3.one * scale;
            yield return null;
        }

        //return to normal size at the end of the animation

        t = 0f;
        while (t < 0.2f)
        {
            t += Time.deltaTime;
            transform.localScale = Vector3.Lerp(Vector3.one * overshootScale, Vector3.one * settleScale, t / 0.2f);
            yield return null;
        }

        transform.localScale = originalScale;
    }
}

