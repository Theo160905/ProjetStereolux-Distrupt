using UnityEngine;

public class Corruption : MonoBehaviour
{
    private float duration = 5f;
    private float elapsedTime = 0f;
    private Vector3 startScale = Vector3.zero;
    private Vector3 targetScale = new Vector3(1f, 1f, 0f);

    void Start()
    {
        transform.localScale = startScale;
    }

    void Update()
    {
        if (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            transform.localScale = Vector3.Lerp(startScale, targetScale, t);
        }
    }
}
