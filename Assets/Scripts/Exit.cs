using UnityEngine;

public class Exit : MonoBehaviour
{    
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<InvokesTriggers>() != null && gameObject.GetComponent<Trigger>() != null && gameObject.GetComponent<Trigger>().triggerOnEnter && other.gameObject.GetComponent<InvokesTriggers>().triggerTags.Contains(GetComponent<Trigger>().requiredInvokerTag))
        {            
            if (other.gameObject.GetComponent<Player>() != null && other.gameObject.GetComponent<Stats>() != null)
            {
                other.gameObject.GetComponent<Stats>().CalculateRank();
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<InvokesTriggers>() != null && gameObject.GetComponent<Trigger>() != null && gameObject.GetComponent<Trigger>().triggerOnExit && other.gameObject.GetComponent<InvokesTriggers>().triggerTags.Contains(GetComponent<Trigger>().requiredInvokerTag))
        {
            if (other.gameObject.GetComponent<Player>() != null && other.gameObject.GetComponent<Stats>() != null)
            {
                other.gameObject.GetComponent<Stats>().CalculateRank();
            }
        }
    }
}
