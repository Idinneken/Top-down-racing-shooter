using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public int requiredControlPointNumber;
    public int currentControlPointNumber;
    internal bool canExitLevel;

    public GameObject FinishTriggerObject;    

    public void CheckIfPlayerCanFinishLevel()
    {
        if (currentControlPointNumber >= requiredControlPointNumber)
        {
            canExitLevel = true;
        }

        if (canExitLevel)
        {
            FinishTriggerObject.SetActive(true);
        }
    }

}
