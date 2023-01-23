using Extensions;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public int requiredControlPointNumber, currentControlPointNumber;
    public int timeBonusThreshold, timeBonusMultiplier;

    public GameObject player, finishTriggerObject, losePanel, gameTimer, losePosition;

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

    public void GameOver()
    {
        finishTriggerObject.SetActive(false);
        losePanel.SetActive(true);
        GetComponent<Timescale>().SetTimescale(0.1f);

        player.GetComponent<Player>().ChangePos_(player.GetComponent<Player>().characterController, losePosition.transform.position);
        player.GetComponent<Player>().characterController.velocity.Set(0, 0, 0);
        GetComponent<Player>().moveSpeed = 0;
    }
}
