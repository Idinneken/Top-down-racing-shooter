using UnityEngine;

public class AffectTimerOnTrigger : MonoBehaviour
{
    public GameObject timerObject;
    internal Timer timer;
    public string action;

    public void Start()
    {
        timer = timerObject.GetComponent<Timer>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<InvokesTriggers>() != null && gameObject.GetComponent<Trigger>() != null && gameObject.GetComponent<Trigger>().triggerOnEnter && other.gameObject.GetComponent<InvokesTriggers>().triggerTags.Contains(GetComponent<Trigger>().requiredInvokerTag))
        {
            AffectTimer(timer);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<InvokesTriggers>() != null && gameObject.GetComponent<Trigger>() != null && gameObject.GetComponent<Trigger>().triggerOnExit && other.gameObject.GetComponent<InvokesTriggers>().triggerTags.Contains(GetComponent<Trigger>().requiredInvokerTag))
        {
            AffectTimer(timer);
        }
    }

    private void AffectTimer(Timer timer_)
    {
        if (action == "pause")
        {
            timer_.PauseTimer();
        }

        if (action == "resume")
        {
            timer_.ResumeTimer();
        }

        if (action == "toggle")
        {
            timer_.ToggleTimer();
        }

        if (action == "start")
        {
            timer_.StartTimer();
        }

        if (action == "reset")
        {
            timer_.ResetTimer(false);
        }

        if (action == "resetImmediate")
        {
            timer_.ResetTimer(true);
        }
    }

}
