using UnityEngine;
using Extensions;

public class ObjectState : MonoBehaviour
{    
    public bool startDisabled = false, toggleAfterDelayOnStart = false, setStateRecursively = false;
    public float toggleOnStartDelay = 0f;
    private bool state = true, eventualState = true;  

    float referencePoint, setStatePoint;
    bool settingStateAfterADelay;

    void Start()
    {        
        if (startDisabled)
        {
            state = false;
        }

        SetState(state);

        if (toggleAfterDelayOnStart)
        {
            ToggleStateAfterDelay(toggleOnStartDelay);
        }
    }    

    public void Update()
    {
        if (settingStateAfterADelay && setStatePoint < Time.fixedTime)
        {            
            SetState(eventualState);
            settingStateAfterADelay = false;     
        }        
    }

    public void SetState(bool state_)
    {
        state = state_;        

        if (setStateRecursively)
        {
            gameObject.SetActiveRecursively_(state);
        }
        else
        {
            gameObject.SetActive(state);
        }
    }

    public void ToggleState()
    {
        state = !state;
        SetState(state);
    }

    public void SetStateAfterDelay(bool state_, float delay_)
    {
        referencePoint = Time.fixedTime;
        setStatePoint = referencePoint + delay_;

        settingStateAfterADelay = true;
        eventualState = state_;
    }

    public void ToggleStateAfterDelay(float delay_)
    {
        eventualState = !state;
        SetStateAfterDelay(eventualState, delay_);        
    }   
}
