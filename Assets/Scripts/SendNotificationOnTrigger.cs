using UnityEngine;

public class SendNotificationOnTrigger : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<InvokesTriggers>() != null && gameObject.GetComponent<Trigger>() != null && gameObject.GetComponent<Trigger>().triggerOnEnter && other.gameObject.GetComponent<InvokesTriggers>().triggerTags.Contains(GetComponent<Trigger>().requiredInvokerTag))
        {
            print("here1");
            foreach (InvokesTriggers invokesTriggers in other.gameObject.GetComponents<InvokesTriggers>())
            {
                print("here2");
                foreach(Component component in invokesTriggers.associatedComponents)
                {
                    print("here3");
                    if (component.GetType() == typeof(Player))
                    {
                        print("here4");
                        Player player = (Player)component;
                        GetComponent<NotifiesPlayer>().SendNotification(player.notification);
                        return;
                    }
                }
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<InvokesTriggers>() != null && gameObject.GetComponent<Trigger>() != null && gameObject.GetComponent<Trigger>().triggerOnEnter && other.gameObject.GetComponent<InvokesTriggers>().triggerTags.Contains(GetComponent<Trigger>().requiredInvokerTag))
        {
            foreach (InvokesTriggers invokesTriggers in other.gameObject.GetComponents<InvokesTriggers>())
            {
                foreach(Component component in invokesTriggers.associatedComponents)
                {
                    if (component.GetType() == typeof(Player))
                    {
                        Player player = (Player)component;
                        GetComponent<NotifiesPlayer>().SendNotification(player.notification);
                        return;
                    }
                }
            }
        }
    }
}
