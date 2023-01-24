using UnityEngine;

public class AffectStatOnTrigger : MonoBehaviour
{            
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<InvokesTriggers>() != null && gameObject.GetComponent<Trigger>() != null && gameObject.GetComponent<Trigger>().triggerOnEnter && other.gameObject.GetComponent<InvokesTriggers>().triggerTags.Contains(GetComponent<Trigger>().requiredInvokerTag))
        {
            Stats agentStats = other.gameObject.GetComponent<Stats>();            
            AffectsStat affectsStat = GetComponent<AffectsStat>();

            foreach (InvokesTriggers invokesTriggers in other.gameObject.GetComponents<InvokesTriggers>())
            {
                foreach (Component component in invokesTriggers.associatedComponents)
                {
                    if (component.GetType() == typeof(Stats))
                    {
                        Stats stats = (Stats)component;
                        stats.stats[affectsStat.statName].ChangeValue(affectsStat.value);
                        return;
                    }
                }
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<InvokesTriggers>() != null && gameObject.GetComponent<Trigger>() != null && gameObject.GetComponent<Trigger>().triggerOnExit && other.gameObject.GetComponent<InvokesTriggers>().triggerTags.Contains(GetComponent<Trigger>().requiredInvokerTag))
        {
            Stats agentStats = other.gameObject.GetComponent<Stats>();
            AffectsStat affectsStat = GetComponent<AffectsStat>();

            foreach (InvokesTriggers invokesTriggers in other.gameObject.GetComponents<InvokesTriggers>())
            {
                foreach (Component component in invokesTriggers.associatedComponents)
                {
                    if (component.GetType() == typeof(Stats))
                    {
                        Stats stats = (Stats)component;
                        stats.stats[affectsStat.statName].ChangeValue(affectsStat.value);
                        return;
                    }
                }
            }
        }
    }

}
