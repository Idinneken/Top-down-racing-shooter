using UnityEngine;

public class ChangeColorOnTrigger : MonoBehaviour
{
    public Color color = Color.white, startColor = Color.white, endColor = Color.green;
    public float transitionDuration = 2f;
    public bool setColor, transitionFromCustomColorToCustomColor, transitionToCustomColorOnly, transitionFromCustomColorOnly;
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<InvokesTriggers>() != null && gameObject.GetComponent<Trigger>() != null && gameObject.GetComponent<Trigger>().triggerOnEnter && other.gameObject.GetComponent<InvokesTriggers>().triggerTags.Contains(GetComponent<Trigger>().requiredInvokerTag))
        {
            foreach (InvokesTriggers invokesTriggers in other.gameObject.GetComponents<InvokesTriggers>())
            {
                foreach (Component component in invokesTriggers.associatedComponents)
                {
                    if (component.GetType() == typeof(CanChangeColor))
                    {
                        CanChangeColor changeColour = (CanChangeColor)component;

                        if (setColor)
                        {
                            changeColour.SetColor(color, false);
                            return;
                        }

                        if (transitionFromCustomColorToCustomColor)
                        {
                            changeColour.TransitionToColor(startColor, endColor, transitionDuration);
                            return;
                        }

                        if (transitionToCustomColorOnly)
                        {
                            changeColour.TransitionToColor(null, endColor, transitionDuration);
                            return;
                        }

                        if (transitionFromCustomColorOnly)
                        {
                            changeColour.TransitionToColor(startColor, null, transitionDuration);
                            return;
                        }
                    }
                }
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<InvokesTriggers>() != null && gameObject.GetComponent<Trigger>() != null && gameObject.GetComponent<Trigger>().triggerOnExit && other.gameObject.GetComponent<InvokesTriggers>().triggerTags.Contains(GetComponent<Trigger>().requiredInvokerTag))
        {
            print("here1");
            foreach (InvokesTriggers invokesTriggers in other.gameObject.GetComponents<InvokesTriggers>())
            {
                print("here2");

                foreach (Component component in invokesTriggers.associatedComponents)
                {
                    print("here3");

                    if (component.GetType() == typeof(CanChangeColor))
                    {
                        print("here4");

                        CanChangeColor changeColour = (CanChangeColor)component;

                        if (setColor)
                        {
                            changeColour.SetColor(color, false);
                            return;
                        }

                        if (transitionFromCustomColorToCustomColor)
                        {
                            changeColour.TransitionToColor(startColor, endColor, transitionDuration);
                            return;
                        }

                        if (transitionToCustomColorOnly)
                        {
                            changeColour.TransitionToColor(null, endColor, transitionDuration);
                            return;
                        }

                        if (transitionFromCustomColorOnly)
                        {
                            changeColour.TransitionToColor(startColor, null, transitionDuration);
                            return;
                        }
                    }
                }
            }
        }
    }
}
