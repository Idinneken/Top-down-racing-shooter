using UnityEngine;

public class Timescale : MonoBehaviour
{
    public float initialTimescale = 1;
    internal float timescale, previousTimescale;

    public void Start()
    {
        SetTimescale(initialTimescale);
    }

    public void SetTimescale(float timescale_)
    {
        previousTimescale = timescale;
        timescale = timescale_;
        Time.timeScale = timescale;
    }    
}
