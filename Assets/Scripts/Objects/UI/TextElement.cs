using TMPro;
using UnityEngine;

public class TextElement : MonoBehaviour
{
    private TMP_Text tmpText;
    private Color tmpTextColor;
    public string initialText = "";
    internal string text;

    public bool startNotVisible;
    private bool visibility = true, eventualVisiblity;
    private bool settingTextVisibilityAfterDelay;
    private float settingTextVisibilityAfterDelayStartTime, settingTextVisibilityAfterDelayDuration;

    internal bool lerpingPosition;
    internal Vector3 startLerpPosition, endLerpPosition;
    internal float lerpStartTime, lerpDuration;

    internal float alpha = 255f;

    void OnEnable()
    {        
        tmpText = GetComponent<TMP_Text>();
        tmpTextColor = tmpText.color;
        SetText(initialText);

        if (startNotVisible)
        {
            SetTextVisibility(false);
        }
    }

    void Update()
    {
        if (lerpingPosition)
        {
            tmpText.rectTransform.anchoredPosition = Vector3.Lerp(startLerpPosition, endLerpPosition, (Time.fixedTime-lerpStartTime)/lerpDuration);

            if (Time.fixedTime >= (lerpStartTime + lerpDuration))
            {                
                lerpingPosition = false;
            }
        }

        if (settingTextVisibilityAfterDelay)
        {
            if (Time.fixedTime >= (settingTextVisibilityAfterDelayStartTime + settingTextVisibilityAfterDelayDuration))
            {
                SetTextVisibility(eventualVisiblity);
                settingTextVisibilityAfterDelay = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            if (alpha == 255f)
            {
                alpha = 0f;
                tmpText.alpha = alpha;
            }
            else
            {
                alpha = 255f;
                tmpText.alpha = alpha;
            }
        }
    }

    public void SetText(string text_)
    {
        text = text_;
        tmpText.text = text_;
    }

    public void SetTextColour(Color color_)
    {
        tmpTextColor = color_;
        tmpText.color = tmpTextColor;
    }

    public void SetTextVisibility(bool visibility_)
    {        
        if (visibility_ == true)
        {
            tmpText.alpha = 255f;
        }
        else 
        {
            tmpText.alpha = 0;
        }

        visibility = visibility_;
    }    

    public void SetTextVisibility(bool visibility_, float delay_)
    {
        settingTextVisibilityAfterDelay = true;
        eventualVisiblity = visibility_;
        settingTextVisibilityAfterDelayStartTime = Time.fixedTime;
        settingTextVisibilityAfterDelayDuration = delay_;
    }

    public void ToggleTextVisibility()
    {
        if(visibility)
        {
            tmpText.alpha = 0;
        }
        else
        {
            tmpText.alpha = 255f;
        }

        visibility = !visibility;
    }

    public void SetTextPosition(Vector3 position_)
    {
        tmpText.rectTransform.anchoredPosition = position_;
    }

    public void LerpToPosition(Vector3 endPosition_, float duration_)
    {
        lerpingPosition = true;
        startLerpPosition = tmpText.rectTransform.anchoredPosition;
        endLerpPosition = endPosition_;
        lerpDuration = duration_;

        lerpStartTime = Time.fixedTime;
    }

    public void LerpToPosition(Vector3 startPosition_, Vector3 endPosition_, float duration_)
    {
        lerpingPosition = true;
        startLerpPosition = startPosition_;
        endLerpPosition = endPosition_;
        lerpDuration = duration_;

        tmpText.rectTransform.anchoredPosition = startLerpPosition;
        lerpStartTime = Time.fixedTime;        
    }
}
