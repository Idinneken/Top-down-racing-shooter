using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extensions;

public class Stats : MonoBehaviour
{
    public Stat health = new(100, 0, 100, false, true);
    public Stat points = new(0, 0, 0, true, false);
    public Stat lives = new(3, 0, 5, false, true);

    public Transform resetPoint;
    
    public void CheckStats()
    {
        if (health.value < health.minValue)
        {
            Die();
        }

        if (lives.value < lives.minValue)
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

    public void Die()
    {
        lives.ChangeValue(-1);
        
        if (lives.value < lives.minValue)
        {
            GameOver();
        }
        else
        {
            ResetPosition();
        }
    }

    public void GameOver()
    {
        print("Game Over");
    }
}

public class Stat
{
    public int value;
    public readonly int startValue, minValue, maxValue;
    public bool clampMinValue, clampMaxValue;
    public bool belowMin = false, aboveMax = false;    

    public Stat(int startValue_)
    {
        startValue = startValue_;
        minValue = default;
        maxValue = default;
        clampMinValue = false;
        clampMaxValue = false;

        value = startValue;
    }

    public Stat(int startValue_, int minValue_, int maxValue_, bool clampMinValue_, bool clampMaxValue_)
    {
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

            if (clampMaxValue)
            {
                value = minValue; 
                belowMin = false;
            }
        }   
    }

}
