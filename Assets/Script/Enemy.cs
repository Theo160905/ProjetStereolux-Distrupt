using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float lifespan = 7f;
    private float timer;
    private bool isAlive = true;

    [SerializeField] Contamination contamination;

    public System.Action<Enemy> OnDestroyed;

    public GameObject CoruptionPrefab; 
    private GameObject corruptionInstance;

    private Button enemyButton;
    private Image buttonImage;
    private Color originalColor;

    void Start()
    {
        contamination = FindFirstObjectByType<Contamination>();

        // Initialisation du bouton
        enemyButton = GetComponent<Button>();
        buttonImage = GetComponent<Image>();

        if (enemyButton != null)
        {
            enemyButton.interactable = false; // Désactive l’interaction pendant 2 sec
            originalColor = buttonImage.color;
            Invoke(nameof(EnableInteraction), 2f); // Appelle EnableInteraction dans 2 sec
        }

        // Instantiate corruption object
        GameObject Canva = GameObject.FindWithTag("Corruption");
        if (Canva != null)
        {
            corruptionInstance = Instantiate(CoruptionPrefab, Canva.transform);
            corruptionInstance.transform.position = transform.position;
        }
        else
        {
            Debug.LogWarning("Canvas with tag 'Canvas' not found. Instantiating corruption without parent.");
            corruptionInstance = Instantiate(CoruptionPrefab);
            corruptionInstance.transform.position = transform.position;
        }
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
