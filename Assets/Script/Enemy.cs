using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float lifespan = 2f;
    private float timer;
    private bool isAlive = true;

    [SerializeField] Contamination contamination;

    public System.Action<Enemy> OnDestroyed;

    void Start()
    {
        contamination = FindFirstObjectByType<Contamination>();
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
        Destroy(gameObject);
    }

    void Respawn()
    {
        timer = 0f;
    }
}
