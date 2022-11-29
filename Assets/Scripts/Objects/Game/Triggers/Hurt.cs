using UnityEngine;

public class Hurt : MonoBehaviour
{
    public bool damage;
    public bool kill;
    public int damageAmount;        

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<InvokesTriggers>() != null && gameObject.GetComponent<Trigger>() != null && gameObject.GetComponent<Trigger>().triggerOnEnter && other.gameObject.GetComponent<InvokesTriggers>().triggerTags.Contains(GetComponent<Trigger>().requiredInvokerTag))
        {
            if (other.gameObject.GetComponent<Stats>() != null)
            {
                if (GetComponent<SetResetPoint>() != null)
                {
                    other.gameObject.GetComponent<Stats>().resetPoint = GetComponent<SetResetPoint>().resetTransform;
                }    

                if (damage)
                {
                    other.gameObject.GetComponent<Stats>().stats["health"].ChangeValue(-damageAmount);
                }

                if (kill)
                {
                    other.gameObject.GetComponent<Stats>().Die();
                }
            }            
        }      
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<InvokesTriggers>() != null && gameObject.GetComponent<Trigger>() != null && gameObject.GetComponent<Trigger>().triggerOnExit && other.gameObject.GetComponent<InvokesTriggers>().triggerTags.Contains(GetComponent<Trigger>().requiredInvokerTag))
        {
            if (other.gameObject.GetComponent<Stats>() != null)
            {
                if (GetComponent<SetResetPoint>() != null)
                {
                    other.gameObject.GetComponent<Stats>().resetPoint = GetComponent<SetResetPoint>().resetTransform;
                }

                if (damage)
                {
                    other.gameObject.GetComponent<Stats>().stats["health"].ChangeValue(-damageAmount);
                }

                if (kill)
                {
                    other.gameObject.GetComponent<Stats>().Die();
                }
            }            
        }      
    }

}
