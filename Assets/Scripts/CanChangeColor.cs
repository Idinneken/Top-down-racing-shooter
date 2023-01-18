using UnityEngine;

public class CanChangeColor : MonoBehaviour
{
    private new Renderer renderer;
    internal Color initialColor, referenceColor, transitionColor, color;
    public Color eventualColor;

    float startTransitionTime, transitionDuration, transitionAmount;
    bool transitioning;

    void Start()
    {
        renderer = GetComponent<Renderer>();
        initialColor = renderer.material.color;
        SetColor(initialColor, false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            TransitionToColor(Color.red, null, 2f);
        }

        if (transitioning)
        {            
            transitionAmount = (Time.fixedTime - startTransitionTime) / transitionDuration;
            transitionColor = referenceColor + ((eventualColor - referenceColor) * transitionAmount);

            SetColor(transitionColor, true);

            if (Time.fixedTime >= startTransitionTime + transitionDuration)
            {
                transitioning = false;
                SetColor(eventualColor, false);
            }
        }
    }    

    public void TransitionToColor(Color? startColor_, Color? endColor_, float duration_)
    {
        transitioning = true;
        transitionAmount = 0f;
        transitionDuration = duration_;
        startTransitionTime = Time.fixedTime;

        if (startColor_ == null && endColor_ != null)
        {
            referenceColor = color;
            eventualColor = (Color)endColor_;
            return;
        }
        else if (startColor_ != null && endColor_ == null)
        {
            referenceColor = (Color)startColor_;
            eventualColor = color;
            SetColor((Color)startColor_, true);
            return;
        }
        else if (startColor_ != null && endColor_ != null)
        {
            referenceColor = (Color)startColor_;
            eventualColor = (Color)endColor_;
            SetColor((Color)startColor_, true);
            return;
        }
    }


    public void SetColor(Color color_, bool transitioning_)
    {
        if (!transitioning_)
        {
            color = color_;
        }
        else
        {
            transitionColor = color_;
        }

        renderer.material.SetColor("_Color", color_);
    }
}
