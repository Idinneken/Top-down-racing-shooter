using UnityEngine;
using System.Collections.Generic;

public class Trigger : MonoBehaviour
{
    public List<string> requiredInvokerTags;
    public bool triggerOnEnter, triggerOnStay, triggerOnExit;
}