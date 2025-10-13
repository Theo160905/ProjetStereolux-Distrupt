using UnityEngine;
using UnityEngine.EventSystems;

public class TapZone : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        Vector2 tapPosition = eventData.position;
        Ray ray = Camera.main.ScreenPointToRay(tapPosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);
        Debug.Log("Tap detected at: " + tapPosition);

        if (hit.collider != null)
        {
            Debug.Log("Hit: " + hit.collider.name);

            Enemy enemy = hit.collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.OnTap();
            }
        }
    }
}

