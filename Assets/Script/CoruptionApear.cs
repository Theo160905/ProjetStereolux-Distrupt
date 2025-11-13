using System.Collections;
using UnityEngine;

public class CoruptionApear : MonoBehaviour
{
    private Vector3 scaleBase;
    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        scaleBase = rectTransform.localScale;
        gameObject.transform.localScale = Vector3.zero;
        StartCoroutine(AppearCoroutine());

    }

    public IEnumerator AppearCoroutine()
    {
        float timer = 0f;
        float duration = 0.5f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            float scale = Mathf.Lerp(0f, 1f, timer / duration);
            gameObject.transform.localScale = scaleBase * scale;
            yield return null;
        }
        gameObject.transform.localScale = scaleBase;
    }
}
