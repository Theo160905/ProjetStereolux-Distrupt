using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float lifespan = 2f;
    private float timer;
    private bool isAlive = true;

    [SerializeField] Contamination contamination;

    public System.Action<Enemy> OnDestroyed;

    public GameObject CoruptionPrefab; 
    private GameObject corruptionInstance;

    void Start()
    {
        contamination = FindFirstObjectByType<Contamination>();
        GameObject Canva = GameObject.FindWithTag("Canvas");
        if (Canva != null)
        {
            corruptionInstance = Instantiate(CoruptionPrefab, Canva.transform);
            corruptionInstance.transform.position = gameObject.transform.position;
        }
        else
        {
            Debug.LogWarning("Canvas with tag 'Canvas' not found. Instantiating corruption without parent.");
            corruptionInstance = Instantiate(CoruptionPrefab);
            corruptionInstance.transform.position = gameObject.transform.position;
        }
    }

    void Update()
    {
        if (!isAlive) return;

        timer += Time.deltaTime;
        if (timer >= lifespan)
        {
            contamination.IncreaseContamination(contamination.contaminationRate);
            Debug.Log("Contamination increased to: " + contamination.GetCurrentContamination());
            Destroy(gameObject);
        }
    }

    public void OnTap()
    {
        if (!isAlive) return;
        isAlive = false;
        OnDestroyed?.Invoke(this);
        Destroy(corruptionInstance);
        Destroy(gameObject);
    }

    void Respawn()
    {
        timer = 0f;
    }
}
