using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button), typeof(Image))]
public class Enemy : MonoBehaviour
{
    [Header("Timings")]
    [Range(0.0f, 5.0f)] public float TimeToBeInteractable = 2f;
    public float lifespan = 1.5f;

    [Header("References")]
    public GameObject corruptionInstance;

    public VFXPool ObjectPoolVFX;

    public event Action<Enemy> OnEnemyFinished;

    private float timer;
    private bool isAlive;
    private Button enemyButton;
    private Image buttonImage;

    void Awake()
    {
        ObjectPoolVFX = FindFirstObjectByType<VFXPool>();

        enemyButton = GetComponent<Button>();
        buttonImage = GetComponent<Image>();
        enemyButton.onClick.AddListener(OnTap);
    }

    void OnEnable()
    {
        InitializeEnemy();
    }

    void InitializeEnemy()
    {
        isAlive = true;
        timer = 0f;
        enemyButton.interactable = false;
        buttonImage.color = Color.white;

        if (corruptionInstance != null)
            corruptionInstance.transform.position = transform.position;

        StartCoroutine(MakeInteractableAfterDelay());
    }

    IEnumerator MakeInteractableAfterDelay()
    {
        yield return new WaitForSeconds(TimeToBeInteractable);
        EnableInteraction();
    }

    void Update()
    {
        if (!isAlive) return;

        timer += Time.deltaTime;
        if (timer >= lifespan)
        {
            isAlive = false;
            ObjectPoolVFX.Spawn("DespawnVFX", transform.position);
            OnEnemyFinished?.Invoke(this);
            InitializeEnemy();
        }
    }

    public void EnableInteraction()
    {
        enemyButton.interactable = true;
        buttonImage.color = new Color(0.5f, 0.75f, 1f);
        ObjectPoolVFX.Spawn("SpawnVFX", transform.position);
    }

    void OnTap()
    {
        if (!isAlive) return;
        isAlive = false;

        ObjectPoolVFX.Spawn("HitVFX", transform.position);

        if (corruptionInstance != null)
            Destroy(corruptionInstance);

        OnEnemyFinished?.Invoke(this);

        Destroy(gameObject, 0.5f);
    }
}
