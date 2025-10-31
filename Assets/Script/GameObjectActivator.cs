using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectActivator : MonoBehaviour
{

    [SerializeField] private List<GameObject> objectsList = new List<GameObject>();
    [SerializeField] private float activationDelay = 1f;

    private int currentIndex = 0;
    private Coroutine activationCoroutine;

    private void Start()
    {
        foreach (var obj in objectsList)
        {
            if (obj != null)
                obj.SetActive(false);
        }
        ActivateCurrentObject();
    }

    private void ActivateCurrentObject()
    {
        if (activationCoroutine != null)
            StopCoroutine(activationCoroutine);

        activationCoroutine = StartCoroutine(ActivateWithDelay());
    }

    private IEnumerator ActivateWithDelay()
    {
        foreach (var obj in objectsList)
        {
            if (obj != null)
                obj.SetActive(false);
        }

        yield return new WaitForSeconds(activationDelay);

        foreach (var obj in objectsList)
        {
            if (obj != null)
                obj.SetActive(false);
        }

        if (objectsList.Count > 0 && objectsList[currentIndex] != null)
            objectsList[currentIndex].SetActive(true);
    }

    public void NextObject()
    {
        if (objectsList.Count == 0) return;

        currentIndex++;
        if (currentIndex >= objectsList.Count)
        {
            currentIndex = 0;
        }
        ActivateCurrentObject();
    }

    public void PreviousObject()
    {
        if (objectsList.Count == 0) return;

        currentIndex--;
        if (currentIndex < 0)
            currentIndex = objectsList.Count - 1;

        ActivateCurrentObject();
    }
}
