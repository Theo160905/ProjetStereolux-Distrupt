using System.Collections;
using System.Collections.Generic;
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
    public EnemyManager enemyManager;

    public List<GameObject> AppearanceImage;
    public GameObject SpawnImage;

    public Sprite spriteHit;


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

        SpawnImage.SetActive(false);
        for (int i = 0; i < AppearanceImage.Count; i++)
        {
            AppearanceImage[i].SetActive(false);
        }
    }

    void OnEnable()
    {
        InitializeEnemy();
    }

    public void InitializeEnemy()
    {
        isAlive = true;
        timer = 0f;
        enemyButton.interactable = false;

        for (int i = 0; i < AppearanceImage.Count; i++)
        {
            AppearanceImage[i].SetActive(true);
        }
        
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
            DisableInteraction();
        }
    }

    public void EnableInteraction()
    {
        enemyButton.interactable = true;
        SpawnImage.SetActive(true);
        SingletonSFX.Instance.PlaySound(SingletonSFX.Instance.SpawnSound, false);
        ObjectPoolVFX.Spawn("SpawnVFX", transform.position);
    }

    public void DisableInteraction()
    {
        enemyButton.interactable = false;
        SpawnImage.SetActive(false);
        for (int i = 0; i < AppearanceImage.Count; i++)
        {
            AppearanceImage[i].SetActive(false);
        }
        ObjectPoolVFX.Spawn("DespawnVFX", transform.position);
        enemyManager.ActivateNextEnemy();
        //StartCoroutine(enemyManager.NextEnemyAfterDelay(1f));
    }

    void OnTap()
    {
        if (!isAlive) return;
        isAlive = false;
        enemyButton.interactable = false;

        for (int i = 0; i < AppearanceImage.Count; i++)
        {
            AppearanceImage[i].SetActive(false);
        }

        SpawnImage.GetComponent<Image>().sprite = spriteHit;
        
        ObjectPoolVFX.Spawn("HitVFX", transform.position);
        SingletonSFX.Instance.PlaySound(SingletonSFX.Instance.HitSound, false);

        if (corruptionInstance != null)
            Destroy(corruptionInstance);

        enemyManager.HandleEnemyFinished(this);

        Destroy(gameObject, 1.5f);
    }
}
