using UnityEngine;

public class LoseLevelOnTimerHitThreshold : MonoBehaviour
{
    public GameObject levelObject;
    internal Level level;

    internal Timer timer;
    public bool triggerOnGreaterThanThreshold, triggerOnLessThanThreshold;
    public float threshold = 0;

    void Start()
    {
        level = levelObject.GetComponent<Level>();
        timer = GetComponent<Timer>();
    }

    void Update()
    {
        if (triggerOnGreaterThanThreshold)
        {
            if (timer.currentTime > threshold)
            {
                level.GameLose();
            }
        }

        if (triggerOnLessThanThreshold)
        {
            if (timer.currentTime < threshold)
            {
                level.GameLose();
            }
        }
    }
}
