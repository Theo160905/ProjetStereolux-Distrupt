using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class AbsSineValue : MonoBehaviour
{
    [SerializeField] private float Amplitude;
    [SerializeField] private float Speed;

    [SerializeField] private List<GameObject> objectsToScale;

    void Update()
    {
        float sizeCurve = Mathf.Sin(Time.fixedTime * Speed);
        float sizeCurveAbs = Mathf.Abs(sizeCurve);
        float sizeModifier = 1f + (sizeCurveAbs * Amplitude);
        foreach (GameObject obj in objectsToScale)
        {
            if (obj != null)
            {
                RectTransform objRectTransform = obj.GetComponent<RectTransform>();
                if (objRectTransform != null)
                {
                    objRectTransform.sizeDelta = new Vector2(100 * sizeModifier, 100 * sizeModifier);
                }
            }
        }
    }
}
