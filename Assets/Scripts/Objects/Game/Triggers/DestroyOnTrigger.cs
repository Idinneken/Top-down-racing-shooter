using System.Collections.Generic;
using UnityEngine;

public class DestroyOnTrigger : MonoBehaviour
{
    public List<GameObject> objectsToDestroy = new();
    public bool destroyThisOnTrigger, destroyObjectsOnTrigger;

    public string requiredComponentName;    

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<InvokesTriggers>() != null && gameObject.GetComponent<Trigger>() != null && gameObject.GetComponent<Trigger>().triggerOnEnter && other.gameObject.GetComponent<InvokesTriggers>().triggerTags.Contains(GetComponent<Trigger>().requiredInvokerTag))
        {            
            if (destroyObjectsOnTrigger)
            {
                foreach(GameObject gameObject in objectsToDestroy)
                {
                    Destroy(gameObject);
                }
            }
            
            if (destroyThisOnTrigger)
            {
                Destroy(gameObject);
                return;
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<InvokesTriggers>() != null && gameObject.GetComponent<Trigger>() != null && gameObject.GetComponent<Trigger>().triggerOnExit && other.gameObject.GetComponent<InvokesTriggers>().triggerTags.Contains(GetComponent<Trigger>().requiredInvokerTag))
        {
            if (destroyObjectsOnTrigger)
            {
                foreach(GameObject gameObject in objectsToDestroy)
                {
                    Destroy(gameObject);
                }
            }
            
            if (destroyThisOnTrigger)
            {
                Destroy(gameObject);
                return;
            }
        }
    }

}
