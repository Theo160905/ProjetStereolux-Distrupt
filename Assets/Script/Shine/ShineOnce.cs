using System.Collections;
using UnityEngine;

public class ShineOnce : MonoBehaviour
{
    [SerializeField] private Material _material;

    [SerializeField][Tooltip ("The number must be very small (ex: 0.001 is a good number)")] private float _howFastItGoes;

    public void PlayShine()
    {
        StartCoroutine(SlowShine());
    }

    IEnumerator SlowShine()
    {
        _material.SetFloat("_MoveShine", 0f);

        for (float i = 0; i < _material.GetFloat("_WaveSpeed"); i += Time.deltaTime)
        {
            _material.SetFloat("_MoveShine", i);
            yield return new WaitForSeconds(_howFastItGoes);
        }

        _material.SetFloat("_MoveShine", 0f);
    }
}
