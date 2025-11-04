using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class VFXPool : MonoBehaviour
{
    [Header("VFX Prefabs")]
    public VisualEffect SpawnVFX;
    public VisualEffect DespawnVFX;
    public VisualEffect HitVFX;

    [Header("Pool Settings")]
    public int poolSizePerVFX = 10;

    private Dictionary<string, Queue<VisualEffect>> poolDictionary;

    private void Awake()
    {
        poolDictionary = new Dictionary<string, Queue<VisualEffect>>();

        InitializePool("SpawnVFX", SpawnVFX);
        InitializePool("DespawnVFX", DespawnVFX);
        InitializePool("HitVFX", HitVFX);
    }

    private void InitializePool(string key, VisualEffect prefab)
    {
        if (prefab == null) return;

        Queue<VisualEffect> objectPool = new Queue<VisualEffect>();

        for (int i = 0; i < poolSizePerVFX; i++)
        {
            VisualEffect obj = Instantiate(prefab, transform);
            obj.gameObject.SetActive(false);
            objectPool.Enqueue(obj);
        }

        poolDictionary.Add(key, objectPool);
    }

    public VisualEffect Spawn(string key, Vector3 position)
    {
        if (!poolDictionary.ContainsKey(key))
        {
            Debug.LogWarning($"Pool '{key}' n'existe pas !");
            return null;
        }

        VisualEffect vfx = poolDictionary[key].Dequeue();
        vfx.transform.position = position;
        vfx.gameObject.SetActive(true);
        vfx.Play();

        StartCoroutine(ReturnToPoolAfterDuration(key, vfx, 1.5f)); 

        return vfx;
    }

    private IEnumerator ReturnToPoolAfterDuration(string key, VisualEffect vfx, float duration)
    {
        yield return new WaitForSeconds(duration);
        vfx.Stop();
        vfx.gameObject.SetActive(false);
        poolDictionary[key].Enqueue(vfx);
    }
}
