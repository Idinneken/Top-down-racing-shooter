using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPoint : MonoBehaviour
{
    public GameObject levelObject;
    internal Level level;
    
    void Start()
    {
        level = levelObject.GetComponent<Level>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<InvokesTriggers>() != null && gameObject.GetComponent<Trigger>() != null && gameObject.GetComponent<Trigger>().triggerOnEnter && GetComponent<Trigger>().requiredInvokerTags.Contains(other.gameObject.GetComponent<InvokesTriggers>().triggerTag))
        {
            level.currentControlPointNumber++;
            level.CheckIfPlayerCanFinishLevel();

        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<InvokesTriggers>() != null && gameObject.GetComponent<Trigger>() != null && gameObject.GetComponent<Trigger>().triggerOnExit && GetComponent<Trigger>().requiredInvokerTags.Contains(other.gameObject.GetComponent<InvokesTriggers>().triggerTag))
        {
            level.currentControlPointNumber++;
            level.CheckIfPlayerCanFinishLevel();
        }
    }

}
