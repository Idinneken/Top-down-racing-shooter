using UnityEngine;

public class ControlPoint : MonoBehaviour
{
    public GameObject levelObject;
    internal Level level;
    
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
            }

            level.currentControlPointNumber++;
            level.CheckIfPlayerCanFinishLevel();

        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<InvokesTriggers>() != null && gameObject.GetComponent<Trigger>() != null && gameObject.GetComponent<Trigger>().triggerOnExit && other.gameObject.GetComponent<InvokesTriggers>().triggerTags.Contains(GetComponent<Trigger>().requiredInvokerTag))
        {
            if (other.gameObject.GetComponent<Stats>() && other.gameObject.GetComponent<Stats>().HasStat("checkpoints"))
            {
                other.gameObject.GetComponent<Stats>().stats["checkpoints"].ChangeValue(1);
            }

            level.currentControlPointNumber++;
            level.CheckIfPlayerCanFinishLevel();
        }
    }

}
