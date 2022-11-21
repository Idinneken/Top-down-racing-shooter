using UnityEngine;
using Extensions;
using System.Collections.Generic;

public class GeoChecker : MonoBehaviour
{
    public LayerMask checkLayer;

    public List<GameObject> touchedObjects = new(); 
    public bool isTouching = false;
    public bool previousFrameIsTouching = false;
    public bool firstFrameTouching = false;
   
    public bool checking = true;

    public bool beingEnabledLater, beingDisabledLater, beingToggledLater;
    public float timeThatStateIsNextBeingChanged;

    public void Update()
    {
        if (beingEnabledLater == true && Time.fixedTime >= timeThatStateIsNextBeingChanged)
        {            
            EnableChecker();
            beingEnabledLater = false;
            timeThatStateIsNextBeingChanged = 0;
        }

        if (beingDisabledLater == true && Time.fixedTime >= timeThatStateIsNextBeingChanged)
        {
            DisableChecker();
            beingDisabledLater = false;
            timeThatStateIsNextBeingChanged = 0;
        }

        if (beingToggledLater == true && Time.fixedTime >= timeThatStateIsNextBeingChanged)
        {
            beingToggledLater = false;
            ToggleChecker();
            timeThatStateIsNextBeingChanged = 0;
        }
        

        firstFrameTouching = false;
        previousFrameIsTouching = isTouching;
        
        if (checking)
        {
            isTouching = CheckTick();

            if (previousFrameIsTouching == false && isTouching)
            {
                firstFrameTouching = true;
            }
        }        
    }    

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.IsOnLayer_(checkLayer))
        {
            // print("adding object");
            touchedObjects.Add(other.gameObject);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.IsOnLayer_(checkLayer))
        {
            touchedObjects.Remove(other.gameObject);
        }
    }

    public bool CheckTick()
    {
        if (touchedObjects.Count > 0)
        {
            return true;
        }
        return false;
    }

    public void EnableChecker()
    {
        checking = true;
    }

    public void DisableChecker()
    {
        checking = false;
    }

    public void ToggleChecker()
    {
        checking = !checking;
    }

    public void EnableCheckerFor(float time_)
    {
        EnableChecker();
        timeThatStateIsNextBeingChanged = Time.fixedTime + time_;
        beingDisabledLater = true; 
        beingEnabledLater = false;
        beingToggledLater = false;
    }

    public void DisableCheckerFor(float time_)
    {
        DisableChecker();
        timeThatStateIsNextBeingChanged = Time.fixedTime + time_;
        beingEnabledLater = true;
        beingDisabledLater = false;
        beingToggledLater = false;
    }

    public void ToggleCheckerFor(float time_)
    {
        ToggleChecker();
        timeThatStateIsNextBeingChanged = Time.fixedTime + time_;        
        beingToggledLater = true;
        beingEnabledLater = false;
        beingDisabledLater = false;
    }
}
