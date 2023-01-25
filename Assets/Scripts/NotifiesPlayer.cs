using UnityEngine;

public class NotifiesPlayer : MonoBehaviour
{
    public string text;
    public Color color = Color.white;
    public float duration = 2f;
    public void SendNotification(TextElement notification_)
    {
        notification_.SetTextVisibility(true);
        notification_.SetText(text);
        notification_.SetTextColour(color);
        notification_.LerpToPosition(new Vector3(0,-64), new Vector3(0,64), 0.125f);
        notification_.SetTextVisibility(false, duration);
    }    
}
