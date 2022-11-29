using System.Collections.Generic;
using UnityEngine;

public class SequenceTrigger : MonoBehaviour
{
    public List<GameObject> triggersInSequence; 

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<InvokesTriggers>() != null && gameObject.GetComponent<Trigger>() != null && gameObject.GetComponent<Trigger>().triggerOnEnter && other.gameObject.GetComponent<InvokesTriggers>().triggerTags.Contains(GetComponent<Trigger>().requiredInvokerTag))
        {
            SetTriggersActive();                     
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<InvokesTriggers>() != null && gameObject.GetComponent<Trigger>() != null && gameObject.GetComponent<Trigger>().triggerOnExit && other.gameObject.GetComponent<InvokesTriggers>().triggerTags.Contains(GetComponent<Trigger>().requiredInvokerTag))
        {
            SetTriggersActive();        
        }
    }

    void SetTriggersActive()
    {
        foreach (GameObject trigger in triggersInSequence)
        {
            trigger.SetActive(true);
        }
    }
}
