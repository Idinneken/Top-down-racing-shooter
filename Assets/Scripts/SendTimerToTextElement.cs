using System;
using UnityEngine;

public class SendTimerToTextElement : MonoBehaviour
{
    public GameObject textElementObject;
    private TextElement textElement;
    private Timer timer;

    void Start()
    {
        textElement = textElementObject.GetComponent<TextElement>();
        timer = GetComponent<Timer>();
    }

    void Update()
    {
        textElement.SetText(Math.Round(timer.currentTime, 2).ToString());
    }
}
