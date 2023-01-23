using UnityEngine;

public class AffectTimescaleOnTrigger : MonoBehaviour
{
    public float value = 1;
    public GameObject timescaleObject;
    private Timescale timescale;

    public void Start()
    {
        timescale = timescaleObject.GetComponent<Timescale>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<InvokesTriggers>() != null && gameObject.GetComponent<Trigger>() != null && gameObject.GetComponent<Trigger>().triggerOnEnter && other.gameObject.GetComponent<InvokesTriggers>().triggerTags.Contains(GetComponent<Trigger>().requiredInvokerTag))
        {
            timescale.SetTimescale(value);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<InvokesTriggers>() != null && gameObject.GetComponent<Trigger>() != null && gameObject.GetComponent<Trigger>().triggerOnExit && other.gameObject.GetComponent<InvokesTriggers>().triggerTags.Contains(GetComponent<Trigger>().requiredInvokerTag))
        {
            timescale.SetTimescale(value);
        }
    }
}
