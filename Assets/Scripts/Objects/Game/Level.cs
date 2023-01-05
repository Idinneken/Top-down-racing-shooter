using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public int requiredControlPointNumber, currentControlPointNumber;
    public int timeBonusThreshold, timeBonusMultiplier;

    public GameObject finishTriggerObject;

    public Dictionary<string, int> rankScorePairs = new();

    public void Start()
    {
        rankScorePairs.Add("S+", 5000);
        rankScorePairs.Add("S", 4500);
        rankScorePairs.Add("A", 4000);
        rankScorePairs.Add("B", 3500);
        rankScorePairs.Add("C", 3000);
        rankScorePairs.Add("D", 2000);
        rankScorePairs.Add("F", 0);
    }

    public void CheckIfPlayerCanFinishLevel()
    {
        if (currentControlPointNumber >= requiredControlPointNumber)
        {
            finishTriggerObject.SetActive(true);
        }

    }
}
