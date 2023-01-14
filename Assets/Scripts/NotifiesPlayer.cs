using UnityEngine;

public class NotifiesPlayer : MonoBehaviour
{
    public GameObject notificationObject;
    internal TextElement notification;
    
    public string text;
    public Color color;
    public float duration;

    public void Start()
    {
        notification = notificationObject.GetComponent<TextElement>();
    }

    public void SendNotification()
    {
        notification.SetTextVisibility(true);
        notification.SetText(text);
        notification.SetTextColour(color);
        notification.LerpToPosition(new Vector3(0,-64), new Vector3(0,64), 0.125f);
        notification.SetTextVisibility(false, 2f);

    }    
}
