using UnityEngine;

public class Timer : MonoBehaviour
{
    private float referencePoint;
    public float startingTime, currentTime;
    public bool countsDown = false, startImmediately = false;
    internal bool paused = true;

    public void Start()
    {
        currentTime = startingTime;
        if (startImmediately)
        {
            StartTimer();
        }
    }

    void FixedUpdate()
    {
        if (!paused)
        {
            if (countsDown)
            {
                currentTime -= Time.fixedTime - referencePoint;
            }
            else
            {
                currentTime += Time.fixedTime - referencePoint;
            }

            referencePoint = Time.fixedTime;
        }
    }

    public void StartTimer()
    {
        referencePoint = Time.fixedTime;
        paused = false;
    }

    public void ResumeTimer()
    {
        paused = false;
    }

    public void PauseTimer()
    {
        paused = true;
    }

    public void ToggleTimer()
    {
        if (paused)
        {
            ResumeTimer();
        }
        else
        {
            PauseTimer();
        }
    }

    public void ToggleCountDirection()
    {
        countsDown = !countsDown;
    }

    public void ResetTimer(bool timerImmediatelyProgresses_)
    {
        currentTime = startingTime;

        if (timerImmediatelyProgresses_)
        {
            StartTimer();
        }
        else
        {
            PauseTimer();
        }
    }
}
