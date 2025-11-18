using UnityEngine;
using System.Collections.Generic;

public class ActivateGO : MonoBehaviour
{
    [SerializeField] private List<GameObject> objectsToActivateInScene = new List<GameObject>();


    void Start()
    {
        foreach (var obj in objectsToActivateInScene)
        {
            if (obj != null)
            obj.SetActive(true);
        }
    }
}
