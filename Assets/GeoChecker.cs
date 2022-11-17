using UnityEngine;
using Extensions;

public class GeoChecker : MonoBehaviour
{
    public Sphere shape;

    public bool isTouching = false;
    public bool previousFrameIsTouching = false;
    public bool firstFrameTouching = false;
    public string componentThatIsCheckedFor;

    public bool checking = true;

    public void Update()
    {
        firstFrameTouching = false;

        if (checking)
        {
            isTouching = CheckTick();

            if (previousFrameIsTouching == false && isTouching)
            {
                firstFrameTouching = true;
            }
        }

        previousFrameIsTouching = isTouching;
    }    

    public bool CheckTick()
    {
        Collider[] collidersOverlappingChecker = Physics.OverlapSphere(shape.transform.position, shape.radius);

        if (collidersOverlappingChecker == null)
        {
            return false;
        }
        else
        {
            foreach(Collider collider in collidersOverlappingChecker)
            {
                if (collider.gameObject.GetComponent(componentThatIsCheckedFor))
                {
                    return true;
                }
            }
        }

        return false;

        //return Physics.CheckSphere(shape.transform.position, shape.radius, checkLayer);
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
        this.WaitForSeconds(time_);
        DisableChecker();
    }

    public void DisableCheckerFor(float time_)
    {
        DisableChecker();
        this.WaitForSeconds(time_);
        EnableChecker();
    }

    public void ToggleCheckerFor(float time_)
    {
        ToggleChecker();
        this.WaitForSeconds(time_);
        ToggleChecker();
    }
}
