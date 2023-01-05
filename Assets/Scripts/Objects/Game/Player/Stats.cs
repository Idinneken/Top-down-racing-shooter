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

    public KeyValuePair<string, int> rankScorePair = new();

    public Dictionary<string, Stat> stats = new();
    public Dictionary<string, Axiom> axioms = new();

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

        axioms.Add("barrelsHaveBeenDestroyed", new(this, null, false));
        axioms.Add("blueCoinsHaveBeenSelected", new(this, null, false));
        axioms.Add("rocksHaveBeenPushed", new(this, null, false));
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

    public void CalculateRank(Exit exit_)
    {
        rankPanel.SetActiveRecursively_(true);

        int timeBonus = (int)(level.timeBonusThreshold - timer.currentTime) * level.timeBonusMultiplier;
        //(60 - 35) = 25. 25 * 100 = 2500
        print("timebonus: " + timeBonus);
        
        if (timeBonus < 0)
        {
            timeBonus = 0;
        }

        stats["score"].value = stats["points"].value + timeBonus;

        Dictionary<string, int> availableRankScorePairs = new();

        foreach (KeyValuePair<string, int> levelRankScorePair in level.rankScorePairs)
        {
            if (levelRankScorePair.Value <= stats["score"].value)
            {
                availableRankScorePairs.Add(levelRankScorePair.Key, levelRankScorePair.Value);
            }
        }

        KeyValuePair<string, int> greatestAvailableRankScorePair = new();

        foreach (KeyValuePair<string, int> availableRankScorePair in availableRankScorePairs)
        {
            print(availableRankScorePair.Key + " " + availableRankScorePair.Value);

            if (availableRankScorePair.Value >= greatestAvailableRankScorePair.Value)
            {
                greatestAvailableRankScorePair = availableRankScorePair;                
            }
        }

        rankScorePair = greatestAvailableRankScorePair;

        print(rankScorePair.Key);
        print(rankScorePair.Value);

        rankText.SetText(rankScorePair.Key);
        scoreText.SetText(stats["score"].value.ToString());
    }

    public void GameOver()
    {
        print("Game Over");
    }
}

public class Axiom
{
    private Stats stats;
    private TextElement textElement;

    public bool state;
    public readonly bool initialState;    

    public Axiom(Stats stats_, TextElement textElement_, bool initialState_)
    {
        stats = stats_;
        textElement = textElement_;

        initialState = initialState_;
        state = initialState;
    }

    public void SetState(bool state_)
    {
        state = state_;

    }

    public void ToggleState()
    {
        state = !state; 
    }

    public void ResetState()
    {
        state = initialState;
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
