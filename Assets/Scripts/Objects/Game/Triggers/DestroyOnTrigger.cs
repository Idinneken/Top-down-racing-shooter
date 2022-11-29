using UnityEngine;

public class DestroyOnTrigger : MonoBehaviour
{
    public GameObject objectToDestroy;

    public string requiredComponentName;    

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<InvokesTriggers>() != null && gameObject.GetComponent<Trigger>() != null && gameObject.GetComponent<Trigger>().triggerOnExit && other.gameObject.GetComponent<InvokesTriggers>().triggerTags.Contains(GetComponent<Trigger>().requiredInvokerTag))
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
        if (other.gameObject.GetComponent<InvokesTriggers>() != null && gameObject.GetComponent<Trigger>() != null && gameObject.GetComponent<Trigger>().triggerOnEnter && other.gameObject.GetComponent<InvokesTriggers>().triggerTags.Contains(GetComponent<Trigger>().requiredInvokerTag))
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
