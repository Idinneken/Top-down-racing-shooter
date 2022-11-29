using UnityEngine;

public class AffectStatOnTrigger : MonoBehaviour
{            
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<InvokesTriggers>() != null && gameObject.GetComponent<Trigger>() != null && gameObject.GetComponent<Trigger>().triggerOnEnter && other.gameObject.GetComponent<InvokesTriggers>().triggerTags.Contains(GetComponent<Trigger>().requiredInvokerTag))
        {
            Stats agentStats = other.gameObject.GetComponent<Stats>() ?? (Stats)other.gameObject.GetComponent<InvokesTriggers>().associatedComponent ?? null;
            AffectsStat affectsStat = GetComponent<AffectsStat>();

            if (agentStats != null && affectsStat != null && agentStats.HasStat(affectsStat.statName))
            {
                agentStats.stats[affectsStat.statName].ChangeValue(affectsStat.value);
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<InvokesTriggers>() != null && gameObject.GetComponent<Trigger>() != null && gameObject.GetComponent<Trigger>().triggerOnExit && other.gameObject.GetComponent<InvokesTriggers>().triggerTags.Contains(GetComponent<Trigger>().requiredInvokerTag))
        {
            Stats agentStats = other.gameObject.GetComponent<Stats>() ?? (Stats)other.gameObject.GetComponent<InvokesTriggers>().associatedComponent ?? null;
            AffectsStat affectsStat = GetComponent<AffectsStat>();

            if (agentStats != null && affectsStat != null && agentStats.HasStat(affectsStat.statName))
            {
                agentStats.stats[affectsStat.statName].ChangeValue(affectsStat.value);
            }
        }
    }

}
