using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float lifespan = 2f;
    private float timer;
    private bool isAlive = true;

    public System.Action<Enemy> OnDestroyed;

    void Update()
    {
        if (!isAlive) return;

        timer += Time.deltaTime;
        if (timer >= lifespan)
        {
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
