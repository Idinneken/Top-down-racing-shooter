using UnityEngine;

public class HurtInvokerOnTrigger : MonoBehaviour
{
    public bool damage;
    public bool kill;
    public int damageAmount;        

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<InvokesTriggers>() != null && gameObject.GetComponent<Trigger>() != null && gameObject.GetComponent<Trigger>().triggerOnEnter && GetComponent<Trigger>().requiredInvokerTags.Contains(other.gameObject.GetComponent<InvokesTriggers>().triggerTag))
        {
            if (other.gameObject.GetComponent<Stats>() != null)
            {
                if (GetComponent<SetInvokerResetPointOnTrigger>() != null)
                {
                    other.gameObject.GetComponent<Stats>().resetPoint = GetComponent<SetInvokerResetPointOnTrigger>().resetTransform;
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
        if (other.gameObject.GetComponent<InvokesTriggers>() != null && gameObject.GetComponent<Trigger>() != null && gameObject.GetComponent<Trigger>().triggerOnExit && GetComponent<Trigger>().requiredInvokerTags.Contains(other.gameObject.GetComponent<InvokesTriggers>().triggerTag))
        {
            if (other.gameObject.GetComponent<Stats>() != null)
            {
                if (GetComponent<SetInvokerResetPointOnTrigger>() != null)
                {
                    other.gameObject.GetComponent<Stats>().resetPoint = GetComponent<SetInvokerResetPointOnTrigger>().resetTransform;
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
