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

        print(textElement.initialText);
        print(timer.countsDown);
    }

    void Update()
    {
        textElement.SetText(timer.currentTime.ToString());
    }
}
