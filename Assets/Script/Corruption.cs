using UnityEngine;

public class Corruption : MonoBehaviour
{
    void Start()
    {
        gameObject.transform.localScale = new Vector3(0f, 0f, 0f);
    }

    void Update()
    {
        if (gameObject.transform.localScale.x < 1f)
        {
            gameObject.transform.localScale += new Vector3(0.01f, 0.01f, 0f);
        }
    }
}
