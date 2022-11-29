using UnityEngine;

public class SetResetPoint : MonoBehaviour
{
    public Transform resetTransform;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<InvokesTriggers>() != null && gameObject.GetComponent<Trigger>() != null && gameObject.GetComponent<Trigger>().triggerOnEnter && other.gameObject.GetComponent<InvokesTriggers>().triggerTags.Contains(GetComponent<Trigger>().requiredInvokerTag))
        {
            if (other.gameObject.GetComponent<Stats>())
            {
                SetStatResetPoint(other.gameObject.GetComponent<Stats>());
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<InvokesTriggers>() != null && gameObject.GetComponent<Trigger>() != null && gameObject.GetComponent<Trigger>().triggerOnExit && other.gameObject.GetComponent<InvokesTriggers>().triggerTags.Contains(GetComponent<Trigger>().requiredInvokerTag))
        {
            if (other.gameObject.GetComponent<Stats>())
            {
                SetStatResetPoint(other.gameObject.GetComponent<Stats>());
            }
        }
    }

    public void SetStatResetPoint(Stats stats_)
    {
        resetTransform.SetParent(null, true);
        stats_.resetPoint = resetTransform;
    }
}
