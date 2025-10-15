using UnityEngine;

public class Contamination : MonoBehaviour
{
    public float contaminationRate = 0.1f; // Rate of contamination increase per second
    public float maxContamination = 100f; // Maximum contamination level
    private float currentContamination = 0f;


    void Update()
    {
        
    }

    public float GetCurrentContamination()
    {
        return currentContamination;
    }

    public void IncreaseContamination(float amount)
    {
        currentContamination += amount;
        if (currentContamination > maxContamination)
            currentContamination = maxContamination;
    }
}
