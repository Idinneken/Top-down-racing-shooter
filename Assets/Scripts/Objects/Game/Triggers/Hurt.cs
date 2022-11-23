using UnityEngine;

public class Hurt : MonoBehaviour
{
    public bool damage;
    public bool kill;
    public int damageAmount;
    // public int lifeLostNumber;

    public Transform resetPoint;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<InvokesTriggers>() != null && gameObject.GetComponent<Trigger>() != null && gameObject.GetComponent<Trigger>().triggerOnEnter && GetComponent<Trigger>().requiredInvokerTags.Contains(other.gameObject.GetComponent<InvokesTriggers>().triggerTag))
        {
            if (other.gameObject.GetComponent<Stats>() != null)
            {
                if (resetPoint != null)
                {
                    other.gameObject.GetComponent<Stats>().resetPoint = resetPoint;
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
        if (other.gameObject.GetComponent<InvokesTriggers>() != null && gameObject.GetComponent<Trigger>() != null && gameObject.GetComponent<Trigger>().triggerOnEnter && GetComponent<Trigger>().requiredInvokerTags.Contains(other.gameObject.GetComponent<InvokesTriggers>().triggerTag))
        {
            if (other.gameObject.GetComponent<Stats>() != null)
            {
                if (resetPoint != null)
                {
                    other.gameObject.GetComponent<Stats>().resetPoint = resetPoint;
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
