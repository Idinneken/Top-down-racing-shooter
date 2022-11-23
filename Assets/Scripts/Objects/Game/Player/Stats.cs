using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extensions;
using Unity.VisualScripting.FullSerializer;

public class Stats : MonoBehaviour
{
    public Dictionary<string, Stat> stats = new();

    //public Stat health = new(100, 0, 100, false, true);
    //public Stat points = new(0, 0, 0, true, false);
    //public Stat lives = new(3, 0, 5, false, true);

    public Transform resetPoint;
    
    public void Start()
    {
        stats.Add("health", new(this, 100, 0, 100, false, true));
        stats.Add("points", new(this, 0, 0, 0, true, false));
        stats.Add("lives", new(this, 3, 0, 5, false, true));
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
        print(stats["points"].value);

        if (stats["health"].belowMin)
        {
            Die();
        }

        if (stats["lives"].belowMin)
        {
            GameOver();
        }
    }

    public void ResetPosition()
    {
        CharacterController characterController = GetComponent<CharacterController>();
        characterController.ChangePos_(resetPoint.position);
        characterController.velocity.Set(0,0,0);
        GetComponent<Player>().moveSpeed = 0;
    }

    public void GameOver()
    {
        print("Game Over");
    }
}

public class Stat
{
    Stats stats;

    public int value;
    public readonly int startValue;
    public int minValue = 0, maxValue = 0;
    public bool clampMinValue = false, clampMaxValue = false;
    public bool belowMin = false, aboveMax = false;    

    public Stat(Stats stats_, int startValue_)
    {
        stats = stats_;

        startValue = startValue_;        
        value = startValue;
    }

    public Stat(Stats stats_, int startValue_, int minValue_, int maxValue_, bool clampMinValue_, bool clampMaxValue_)
    {
        stats = stats_;

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
        Debug.Log("yuh");
        CheckValue();        
    }

    public void ResetValue(int amount_)
    {
        value = startValue;
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
