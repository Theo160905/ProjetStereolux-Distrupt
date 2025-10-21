using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Range(0.0f, 5.0f)]
    public float TimeToBeInteractable = 2f;

    public float lifespan = 7f;
    private float timer;
    private bool isAlive = true;

    [SerializeField] Contamination contamination;

    public System.Action<Enemy> OnDestroyed;

    public GameObject CoruptionPrefab;
    private GameObject corruptionInstance;

    private Button enemyButton;
    private Image buttonImage;

    void Start()
    {
        InitializeEnemy();
    }

    void InitializeEnemy()
    {
        contamination = FindFirstObjectByType<Contamination>();

        enemyButton = GetComponent<Button>();
        buttonImage = GetComponent<Image>();

        if (enemyButton != null)
        {
            enemyButton.interactable = false;
            Invoke(nameof(EnableInteraction), TimeToBeInteractable);
        }

        GameObject Parent = GameObject.FindWithTag("Corruption");
        if (Parent  != null && corruptionInstance == null)
        {
            corruptionInstance = Instantiate(CoruptionPrefab, Parent .transform);
        }
        else if (corruptionInstance == null)
        {
            corruptionInstance = Instantiate(CoruptionPrefab);
        }

        if (corruptionInstance != null)
        {
            corruptionInstance.transform.position = transform.position;
        }

        timer = 0f;
        isAlive = true;
    }

    void EnableInteraction()
    {
        if (enemyButton != null)
        {
            enemyButton.interactable = true;

            if (buttonImage != null)
                buttonImage.color = new Color(0.5f, 0.75f, 1f);
        }
    }

    void Update()
    {
        if (!isAlive) return;

        timer += Time.deltaTime;
        if (timer >= lifespan)
        {
            contamination.IncreaseContamination(contamination.contaminationRate);
            ResetEnemy();
        }
    }

    public void OnTap()
    {
        if (!isAlive) return;

        isAlive = false;
        OnDestroyed?.Invoke(this);

        if (corruptionInstance != null)
        {
            Destroy(corruptionInstance);
            Destroy(gameObject);
        }
            

    }

    void ResetEnemy()
    {
        gameObject.SetActive(false);

        Invoke(nameof(ReloadEnemy), 1f);
    }

    void ReloadEnemy()
    {
        gameObject.SetActive(true);

        InitializeEnemy();
    }
}
