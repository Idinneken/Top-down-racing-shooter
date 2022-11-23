using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetResetPoint : MonoBehaviour
{
    public Transform resetTransform;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<InvokesTriggers>() != null && gameObject.GetComponent<Trigger>() != null && gameObject.GetComponent<Trigger>().triggerOnEnter && GetComponent<Trigger>().requiredInvokerTags.Contains(other.gameObject.GetComponent<InvokesTriggers>().triggerTag))
        {
            if (other.gameObject.GetComponent<Stats>())
            {
                SetStatResetPoint(other.gameObject.GetComponent<Stats>());
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<InvokesTriggers>() != null && gameObject.GetComponent<Trigger>() != null && gameObject.GetComponent<Trigger>().triggerOnExit && GetComponent<Trigger>().requiredInvokerTags.Contains(other.gameObject.GetComponent<InvokesTriggers>().triggerTag))
        {
            if (other.gameObject.GetComponent<Stats>())
            {
                SetStatResetPoint(other.gameObject.GetComponent<Stats>());
            }
        }
    }

    public void SetStatResetPoint(Stats stats_)
    {
        stats_.resetPoint = resetTransform;
    }
}
