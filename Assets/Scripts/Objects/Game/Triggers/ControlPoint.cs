using Abertay.Analytics;
using System.Collections.Generic;
using UnityEngine;

public class ControlPoint : MonoBehaviour
{
    public GameObject levelObject;
    internal Level level;
    public string controlpointName;
    
    void Start()
    {
        level = levelObject.GetComponent<Level>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<InvokesTriggers>() != null && gameObject.GetComponent<Trigger>() != null && gameObject.GetComponent<Trigger>().triggerOnEnter && other.gameObject.GetComponent<InvokesTriggers>().triggerTags.Contains(GetComponent<Trigger>().requiredInvokerTag))
        {
            if(other.gameObject.GetComponent<Stats>() && other.gameObject.GetComponent<Stats>().HasStat("checkpoints"))
            {
                other.gameObject.GetComponent<Stats>().stats["checkpoints"].ChangeValue(1);
                level.currentControlPointNumber++;
                level.CheckIfPlayerCanFinishLevel();
                ControlPointTriggered(other.gameObject.GetComponent<Stats>());
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<InvokesTriggers>() != null && gameObject.GetComponent<Trigger>() != null && gameObject.GetComponent<Trigger>().triggerOnExit && other.gameObject.GetComponent<InvokesTriggers>().triggerTags.Contains(GetComponent<Trigger>().requiredInvokerTag))
        {
            if (other.gameObject.GetComponent<Stats>() && other.gameObject.GetComponent<Stats>().HasStat("checkpoints"))
            {
                other.gameObject.GetComponent<Stats>().stats["checkpoints"].ChangeValue(1);
                level.currentControlPointNumber++;
                level.CheckIfPlayerCanFinishLevel();
                ControlPointTriggered(other.gameObject.GetComponent<Stats>());
            }
        }
    }

    public void ControlPointTriggered(Stats stats_)
    {
        //print(stats_);
        //print("controlPoint " + controlpointName);
        
        Dictionary<string, object> controlPointParameters = new Dictionary<string, object>()
        {
            {"controlPoint", controlpointName},
            {"controlPointsTriggeredAtPointOfTrigger", stats_.stats["checkpoints"].value},
            {"currentTimeAtPointOfTrigger", stats_.timer.currentTime},
            {"playerPointsAtPointOfTrigger", stats_.stats["points"].value}
        };

        AnalyticsManager.SendCustomEvent("ControlPointTriggered", controlPointParameters);
    }
}
