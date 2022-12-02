using System.Collections.Generic;
using UnityEngine;
using Extensions;

public class SetObjectsActiveOnTrigger : MonoBehaviour
{
    public bool state = true, affectChildren;
    public List<GameObject> gameObjects;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<InvokesTriggers>() != null && gameObject.GetComponent<Trigger>() != null && gameObject.GetComponent<Trigger>().triggerOnEnter && other.gameObject.GetComponent<InvokesTriggers>().triggerTags.Contains(GetComponent<Trigger>().requiredInvokerTag))
        {
            if (affectChildren)
            {
                foreach (GameObject gameObject in gameObjects)
                {
                    gameObject.SetActiveRecursively_(state);
                }
            }
            else
            {
                foreach (GameObject gameObject in gameObjects)
                {
                    gameObject.SetActive(state);
                }
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<InvokesTriggers>() != null && gameObject.GetComponent<Trigger>() != null && gameObject.GetComponent<Trigger>().triggerOnExit && other.gameObject.GetComponent<InvokesTriggers>().triggerTags.Contains(GetComponent<Trigger>().requiredInvokerTag))
        {
            if (affectChildren)
            {
                foreach (GameObject gameObject in gameObjects)
                {
                    gameObject.SetActiveRecursively_(state);
                }
            }
            else
            {
                foreach (GameObject gameObject in gameObjects)
                {
                    gameObject.SetActive(state);
                }
            }
        }
    }
}
