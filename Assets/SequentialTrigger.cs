using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequentialTrigger : MonoBehaviour
{
    public List<GameObject> triggers;
    public string requiredComponentname;
    public bool triggerOnEnter, triggerOnExit; 

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent(requiredComponentname) != null)
        {
            SetTriggersActive();
            gameObject.SetActive(false);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent(requiredComponentname) != null)
        {
            SetTriggersActive();
            gameObject.SetActive(false);
        }
    }

    void SetTriggersActive()
    {
        foreach (GameObject trigger in triggers)
        {
            trigger.SetActive(true);
        }
    }
}
