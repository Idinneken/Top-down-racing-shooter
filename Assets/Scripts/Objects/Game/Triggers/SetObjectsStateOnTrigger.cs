using System.Collections.Generic;
using UnityEngine;

public class SetObjectsStateOnTrigger : MonoBehaviour
{
    public bool setActive;
    public List<GameObject> objectsInSequence; 

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<InvokesTriggers>() != null && gameObject.GetComponent<Trigger>() != null && gameObject.GetComponent<Trigger>().triggerOnEnter && other.gameObject.GetComponent<InvokesTriggers>().triggerTags.Contains(GetComponent<Trigger>().requiredInvokerTag))
        {
            SetObjectsState();                     
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<InvokesTriggers>() != null && gameObject.GetComponent<Trigger>() != null && gameObject.GetComponent<Trigger>().triggerOnExit && other.gameObject.GetComponent<InvokesTriggers>().triggerTags.Contains(GetComponent<Trigger>().requiredInvokerTag))
        {
            SetObjectsState();        
        }
    }

    void SetObjectsState()
    {
        foreach (GameObject trigger in objectsInSequence)
        {
            trigger.SetActive(setActive);
        }
    }
}
