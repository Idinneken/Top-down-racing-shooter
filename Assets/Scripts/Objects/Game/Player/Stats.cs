using System.Collections.Generic;
using UnityEngine;
using Extensions;
using System;

public class Stats : MonoBehaviour
{
    public GameObject levelObject;
    internal Level level;

    internal GameObject timerObject;
    internal Timer timer;

    public GameObject pointsTextObject, livesTextObject, timeTextObject, checkpointTextObject, rankTextObject, scoreTextObject;
    internal TextElement pointsText, livesText, timeText, checkpointText, rankText, scoreText;

    public GameObject rankPanel;

    public KeyValuePair<string, int> rankValuePair = new();

    public Dictionary<string, Stat> stats = new();    

    public Transform initialResetPoint;
    
    public void Start()
    {
        level = levelObject.GetComponent<Level>();

        timerObject = gameObject;
        timer = timerObject.GetComponent<Timer>();

        pointsText = pointsTextObject.GetComponent<TextElement>();
        livesText = livesTextObject.GetComponent<TextElement>();
        timeText = timeTextObject.GetComponent<TextElement>();
        checkpointText = checkpointTextObject.GetComponent<TextElement>();
        scoreText = scoreTextObject.GetComponent<TextElement>();
        rankText = rankTextObject.GetComponent<TextElement>();

        stats.Add("points", new(this, pointsText, 0, 0, 0, true, false));
        stats.Add("lives", new(this, livesText, 3, 0, 5, false, true));
        stats.Add("checkpoints", new(this, checkpointText, 0));
        stats.Add("score", new(this, scoreText, 0));        
    }

    public void Update()
    {
        timeText.SetText(MathF.Round(timer.currentTime).ToString());
    }

    public bool HasStat(string statName)
    {
        return stats.ContainsKey(statName);
    }

    public void Die()
    {
        stats["lives"].ChangeValue(-1);
        ResetPosition();
    }

    public void CheckStats()
    {        
        if (stats["lives"].belowMin)
        {
            GameOver();
        }
    }

    public void ResetPosition()
    {
        CharacterController characterController = GetComponent<CharacterController>();
        characterController.ChangePos_(initialResetPoint.position);
        characterController.velocity.Set(0,0,0);
        GetComponent<Player>().moveSpeed = 0;
    }

    public void CalculateRank()
    {
        rankPanel.SetActiveRecursively_(true);

        int timeBonus = (int)(timer.currentTime - level.timeBonusThreshold) * level.timeBonusMultiplier;
        
        if (timeBonus < 0)
        {
            timeBonus = 0;
        }

        stats["score"].value = stats["points"].value + (timeBonus);

        Dictionary<string, int> availableRankScorePairs = new();

        foreach (KeyValuePair<string, int> rankScorePair in level.rankScorePairs)
        {
            if (rankScorePair.Value <= stats["score"].value)
            {
                availableRankScorePairs.Add(rankScorePair.Key, rankScorePair.Value);
            }
        }

        KeyValuePair<string, int> greatestAvailableRankScorePair = new();

        foreach (KeyValuePair<string, int> rankScorePair in availableRankScorePairs)
        {
            print(rankScorePair.Key + " " + rankScorePair.Value);

            if (rankScorePair.Value > greatestAvailableRankScorePair.Value)
            {
                greatestAvailableRankScorePair = rankScorePair;                
            }
        }

        rankValuePair = greatestAvailableRankScorePair;

        print(rankValuePair.Key);
        print(rankValuePair.Value);

        rankText.SetText(rankValuePair.Key);
        scoreText.SetText(rankValuePair.Value.ToString());
    }

    public void GameOver()
    {
        print("Game Over");
    }
}

public class Stat
{
    private Stats stats;
    private TextElement textElement;

    public int value;
    public readonly int startValue;
    public int minValue = 0, maxValue = 0;
    public bool clampMinValue = false, clampMaxValue = false;
    public bool belowMin = false, aboveMax = false;    

    public Stat(Stats stats_, TextElement textElement_, int startValue_)
    {
        stats = stats_;
        textElement = textElement_;

        startValue = startValue_;        
        value = startValue;
    }

    public Stat(Stats stats_, TextElement textElement_, int startValue_, int minValue_, int maxValue_, bool clampMinValue_, bool clampMaxValue_)
    {
        stats = stats_;
        textElement = textElement_;

        startValue = startValue_;
        minValue = minValue_;
        maxValue = maxValue_;
        clampMinValue = clampMinValue_;
        clampMaxValue = clampMaxValue_;
        value = startValue;
    }

    public void ChangeValue(int amount_)
    {
        value += amount_;
        textElement?.SetText(value.ToString());
        CheckValue();
    }

    public void ResetValue()
    {
        value = startValue;
        textElement?.SetText(value.ToString());
        CheckValue();
    }

    private void CheckValue()
    {
        aboveMax = false;
        belowMin = false;

        if (value > maxValue)
        {
            aboveMax = true;

            if (clampMaxValue)
            {
                value = maxValue; 
                aboveMax = false;
            }
        }   

        if (value < minValue)
        {
            belowMin = true;

            if (clampMinValue)
            {
                value = minValue; 
                belowMin = false;
            }
        }        

        stats.CheckStats();
    }

}
