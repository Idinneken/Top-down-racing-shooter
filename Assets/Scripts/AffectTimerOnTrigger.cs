using UnityEngine;

public class AffectTimerOnTrigger : MonoBehaviour
{        
    public string action;    

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<InvokesTriggers>() != null && gameObject.GetComponent<Trigger>() != null && gameObject.GetComponent<Trigger>().triggerOnEnter && other.gameObject.GetComponent<InvokesTriggers>().triggerTags.Contains(GetComponent<Trigger>().requiredInvokerTag))
        {
            if (other.gameObject.GetComponent<Timer>() != null || other.gameObject.GetComponentInChildren<Timer>() != null)
            {
                AffectTimer(other.gameObject.GetComponent<Timer>());
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<InvokesTriggers>() != null && gameObject.GetComponent<Trigger>() != null && gameObject.GetComponent<Trigger>().triggerOnExit && other.gameObject.GetComponent<InvokesTriggers>().triggerTags.Contains(GetComponent<Trigger>().requiredInvokerTag))
        {
            if (other.gameObject.GetComponent<Timer>() != null || other.gameObject.GetComponentInChildren<Timer>() != null)
            {
                AffectTimer(other.gameObject.GetComponent<Timer>());
            }
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
