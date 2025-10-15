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

    public void DecreaseContamination(float amount)
    {
        currentContamination -= amount;
        if (currentContamination < 0f)
            currentContamination = 0f;
    }

    public void ResetContamination()
    {
        currentContamination = 0f;
    }

    public float SetContamination(float nbEnemies, float lifetime)
    {
        contaminationRate = (100 / nbEnemies);
        return contaminationRate;
    }
}
