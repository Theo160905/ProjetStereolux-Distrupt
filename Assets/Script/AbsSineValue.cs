using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class AbsSineValue : MonoBehaviour
{
    [SerializeField] private float Amplitude;
    [SerializeField] private float Speed;

    void Update()
    {
        float sizeCurve = Mathf.Sin(Time.fixedTime * Speed);
        float sizeCurveAbs = Mathf.Abs(sizeCurve);
        float sizeModifier = 1f + (sizeCurveAbs * Amplitude);
        RectTransform rectTransform = GetComponent<RectTransform>();
        //rectTransform.localScale = new Vector3(sizeModifier, sizeModifier, sizeModifier);
        rectTransform.sizeDelta = new Vector2(100 * sizeModifier, 100 * sizeModifier);
    }
}
