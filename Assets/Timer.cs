using UnityEngine;

public class Timer : MonoBehaviour
{
    private float referencePoint;
    public float currentTime;
    public bool paused = true;
    public bool startImmediately = false;

    public void Start()
    {
        if (startImmediately)
        {
            StartTimer();
        }
    }

    void Update()
    {
        if (!paused)
        {
            currentTime = Time.fixedTime - referencePoint;
        }
    }

    private void StartTimer()
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

    public void ResetTimer(bool timerImmediatelyProgresses)
    {
        referencePoint = Time.fixedTime;
        
        if (timerImmediatelyProgresses)
        {
            StartTimer();
        }
        else
        {
            paused = true;
            currentTime = 0f;
        }
    }
}
