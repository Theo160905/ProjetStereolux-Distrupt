using System.Collections;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;
using UnityEngine.VFX;

public class Enemy : MonoBehaviour
{
    [Range(0.0f, 5.0f)]
    public float TimeToBeInteractable = 2f;

    public float lifespan = 7f;

    public System.Action<Enemy> OnDestroyed;

    public GameObject corruptionInstance;

    private float timer;
    private bool isAlive = true;
    private Button enemyButton;
    private Image buttonImage;

    [Header("VFX")]
    public VisualEffect SpawnVFX;
    public VisualEffect DespawnVFX;
    public VisualEffect HitVFX;
    

    void Start()
    {
        InitializeEnemy();
    }

    void InitializeEnemy()
    {
        enemyButton = GetComponent<Button>();
        buttonImage = GetComponent<Image>();

        if (enemyButton != null)
        {
            enemyButton.interactable = false;
            Invoke(nameof(EnableInteraction), TimeToBeInteractable);
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
            StartCoroutine(WaitUnitilVFXComplete(SpawnVFX));

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
            StartCoroutine(WaitUnitilVFXComplete(DespawnVFX));
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
            StartCoroutine(WaitUnitilVFXComplete(HitVFX));
            Destroy(corruptionInstance);
            Destroy(gameObject);
            Destroy(gameObject.transform.parent.gameObject);
        }
            

    }

    void ResetEnemy()
    {
        StartCoroutine(WaitUnitilVFXComplete(DespawnVFX));

        Invoke(nameof(ReloadEnemy), 1f);
    }

    void ReloadEnemy()
    {
        gameObject.SetActive(true);

        InitializeEnemy();
    }

    IEnumerator WaitUnitilVFXComplete(VisualEffect vfx)
    {
        vfx.gameObject.SetActive(true);
        vfx.Play();
        yield return new WaitForSeconds(2f);
        vfx.gameObject.SetActive(false);
    }

    IEnumerator waitAndDestroy(float delay)
    {
        yield return new WaitForSeconds(2f);
                    Destroy(corruptionInstance);
            Destroy(gameObject);
            Destroy(gameObject.transform.parent.gameObject);

    }
}
