using System.Collections.Generic;
using Abertay.Analytics;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public GameObject levelObject;
    internal Level level;

    public void Start()
    {
        level = levelObject.GetComponent<Level>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<InvokesTriggers>() != null && gameObject.GetComponent<Trigger>() != null && gameObject.GetComponent<Trigger>().triggerOnEnter && other.gameObject.GetComponent<InvokesTriggers>().triggerTags.Contains(GetComponent<Trigger>().requiredInvokerTag))
        {    
            if (other.gameObject.GetComponent<Player>() != null && other.gameObject.GetComponent<Stats>() != null)
            {                
                level.GameWin();
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<InvokesTriggers>() != null && gameObject.GetComponent<Trigger>() != null && gameObject.GetComponent<Trigger>().triggerOnExit && other.gameObject.GetComponent<InvokesTriggers>().triggerTags.Contains(GetComponent<Trigger>().requiredInvokerTag))
        {
            if (other.gameObject.GetComponent<Player>() != null && other.gameObject.GetComponent<Stats>() != null)
            {
                level.GameWin(); 
            }
        }
    }

    
}
