using Extensions;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public int requiredControlPointNumber, currentControlPointNumber;
    public int timeBonusThreshold, timeBonusMultiplier;

    [Space]
    public GameObject playerObject;
    public GameObject finishTriggerObject;
    public GameObject gameTimerObject;
    public GameObject losePositionObject;
    internal Player player;
    internal Stats playerStats;
    internal Timer gameTimer;

    [Space]
    public GameObject winPanelObject;
    public GameObject losePanelObject;

    [Space]
    public GameObject rankTextObject;
    public GameObject scoreTextObject;
    internal TextElement rankText, scoreText;

    internal Dictionary<string, int> rankScorePairs = new();

    public void Start()
    {
        player = playerObject.GetComponent<Player>();
        playerStats = playerObject.GetComponent<Stats>();
        gameTimer = gameTimerObject.GetComponent<Timer>();

        rankText = rankTextObject.GetComponent<TextElement>();
        scoreText = scoreTextObject.GetComponent<TextElement>();

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

    public void GameWin()
    {
        winPanelObject.SetActiveRecursively_(true);

        CalculatePlayerScore();
        CalculateRank();

        winPanelObject.SetActive(true);

        rankText.SetText(playerStats.rankScorePair.Key);
        scoreText.SetText(playerStats.stats["score"].value.ToString());
    }

    public void GameLose()
    {
        finishTriggerObject.SetActive(false);
        losePanelObject.SetActive(true);

        GetComponent<Timescale>().SetTimescale(0.1f);
        gameTimerObject.GetComponent<Timer>().PauseTimer();

        player.ChangePos_(player.characterController, losePositionObject.transform.position);
        player.characterController.velocity.Set(0, 0, 0);
        player.moveSpeed = 0;
    }

    public void CalculatePlayerScore()
    {
        int timeBonus = (int)(gameTimer.currentTime - timeBonusThreshold) * timeBonusMultiplier;
        //(90 - 60) = 30. 30 * 100 = 3000

        if (timeBonus < 0)
        {
            timeBonus = 0;
        }

        playerStats.stats["score"].value = playerStats.stats["points"].value + timeBonus;
    }

    public void CalculateRank()
    {
        Dictionary<string, int> availableRankScorePairs = new();

        foreach (KeyValuePair<string, int> levelRankScorePair in rankScorePairs)
        {
            if (levelRankScorePair.Value <= playerStats.stats["score"].value)
            {
                availableRankScorePairs.Add(levelRankScorePair.Key, levelRankScorePair.Value);
            }
        }

        KeyValuePair<string, int> greatestAvailableRankScorePair = new KeyValuePair<string, int>("null", 0);

        foreach (KeyValuePair<string, int> availableRankScorePair in availableRankScorePairs)
        {
            if (availableRankScorePair.Value >= greatestAvailableRankScorePair.Value)
            {
                greatestAvailableRankScorePair = availableRankScorePair;
            }
        }

        playerStats.rankScorePair = greatestAvailableRankScorePair;        
    }
}
