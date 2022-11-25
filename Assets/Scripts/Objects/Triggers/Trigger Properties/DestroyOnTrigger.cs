using UnityEngine;

public class DestroyOnTrigger : MonoBehaviour
{
    public GameObject objectToDestroy;

    public string requiredComponentName;    

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<InvokesTriggers>() != null && gameObject.GetComponent<Trigger>() != null && gameObject.GetComponent<Trigger>().triggerOnEnter && GetComponent<Trigger>().requiredInvokerTags.Contains(other.gameObject.GetComponent<InvokesTriggers>().triggerTag))
        {            
            if (objectToDestroy == null)
            {
                Destroy(gameObject);
                return;
            }

            Destroy(objectToDestroy);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<InvokesTriggers>() != null && gameObject.GetComponent<Trigger>() != null && gameObject.GetComponent<Trigger>().triggerOnExit && GetComponent<Trigger>().requiredInvokerTags.Contains(other.gameObject.GetComponent<InvokesTriggers>().triggerTag))
        {
            if (objectToDestroy == null)
            {
                Destroy(gameObject);
                return;
            }

            Destroy(objectToDestroy);
        }
    }

}
