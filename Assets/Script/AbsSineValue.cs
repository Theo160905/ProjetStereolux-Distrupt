using UnityEngine;
using UnityEngine.Rendering;

public class AbsSineValue : MonoBehaviour
{
    [SerializeField] private float Amplitude;
    [SerializeField] private float Speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float sizeCurve = Mathf.Sin(Time.fixedTime * Speed);
        float sizeCurveAbs = Mathf.Abs(sizeCurve);
        float sizeModifier = 1f + (sizeCurveAbs * Amplitude);
        transform.localScale = new Vector3(sizeModifier, sizeModifier, sizeModifier);
    }
    private void FixedUpdate()
    {
  
    }
}
