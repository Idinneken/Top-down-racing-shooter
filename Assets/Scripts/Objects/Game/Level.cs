using Abertay.Analytics;
using Extensions;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public bool timeTrialMode, adventureMode;
        
    public int requiredControlPointNumber, currentControlPointNumber, requiredPointsQuantity, currentPointsQuantity;
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

    internal bool gameWon = false;
    internal Dictionary<string, int> rankScorePairs = new();

    private float sendNotificationTime, sendNotficationDelay = 2f;
    private bool sendingNotification = false, sentNotification = false;    

    public void Start()
    {
        if (timeTrialMode)
        {
            gameTimer = gameTimerObject?.GetComponent<Timer>();
        }

        player = playerObject.GetComponent<Player>();
        playerStats = playerObject.GetComponent<Stats>();

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
        if (timeTrialMode)
        {
            if (currentControlPointNumber >= requiredControlPointNumber)
            {
                sendingNotification = true;
                sendNotificationTime = Time.fixedTime + sendNotficationDelay;
                finishTriggerObject.SetActive(true);
            }
            
        }

        if (adventureMode)
        {
            if (currentPointsQuantity >= requiredPointsQuantity)
            {
                sendingNotification = true;
                sendNotificationTime = Time.fixedTime + sendNotficationDelay;
                finishTriggerObject.SetActive(true);
            }
        }

    }

    public void Update()
    {
        if (sendingNotification && Time.fixedTime > sendNotificationTime && !sentNotification)
        {
            GetComponent<NotifiesPlayer>().SendNotification(player.notification);
            sendingNotification = false;
            sentNotification = true;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            GameWin();
        }
    }

    public void GameWin()
    {
        gameWon = true;
        winPanelObject.SetActiveRecursively_(true);

        CalculatePlayerScore();
        CalculateRank();

        winPanelObject.SetActive(true);

        rankText.SetText(playerStats.rankScorePair.Key);
        scoreText.SetText(playerStats.stats["score"].value.ToString());
        GameCompleted(playerStats);
    }

    public void GameLose()
    {
        gameWon = false;
        finishTriggerObject.SetActive(false);
        losePanelObject.SetActive(true);

        GetComponent<Timescale>().SetTimescale(0.1f);
        gameTimer?.PauseTimer();

        player.ChangePos_(player.characterController, losePositionObject.transform.position);
        player.characterController.velocity.Set(0, 0, 0);
        player.moveSpeed = 0;

        GameCompleted(playerStats);
    }

    public void CalculatePlayerScore()
    {
        if (timeTrialMode)
        {
            int timeBonus = (int)(gameTimer.currentTime - timeBonusThreshold) * timeBonusMultiplier;
            //(90 - 60) = 30. 30 * 100 = 3000

            if (timeBonus < 0)
            {
                timeBonus = 0;
            }

            playerStats.stats["score"].value = playerStats.stats["points"].value + timeBonus;
        }

        if (adventureMode)
        {
            playerStats.stats["score"].value = playerStats.stats["points"].value;
        }

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

    public void GameCompleted(Stats stats_)
    {
        string modeLabel = default;

        if (timeTrialMode) { modeLabel = "timeTrialMode"; }
        if (adventureMode) { modeLabel = "adventureMode"; }

        Dictionary<string, object> parameters = new()
        {
            {"gameWon", gameWon},
            {"levelType", modeLabel},
            {"controlPointsTriggered", stats_.stats["checkpoints"].value},            
            {"playerPoints", stats_.stats["points"].value},
            {"playerScore", stats_.stats["score"].value},
            {"playerRank", stats_.rankScorePair.Key}
        };

        if (timeTrialMode)
        {
            parameters.Add("currentTime", stats_.level.gameTimer.currentTime);
        }


        AnalyticsManager.SendCustomEvent("GameCompleted", parameters);
    }
}
