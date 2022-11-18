using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    public bool damageOnEnter, damageOnExit, killOnEnter, killOnExit;
    public int damage, lifeLostNumber;

    public Transform resetPoint;

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.GetComponent<Stats>() != null && damageOnEnter)
        {
            print("here");
            collider.gameObject.GetComponent<Stats>().health.ChangeValue(-damage);
        }
        
        if (collider.gameObject.GetComponent<Stats>() != null && killOnEnter)
        {
            if (resetPoint != null)
            {
                collider.gameObject.GetComponent<Stats>().resetPoint = resetPoint;
            }

            collider.gameObject.GetComponent<Stats>().Die();
        }
    }

    public void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.GetComponent<Stats>() != null && damageOnExit)
        {
            collider.gameObject.GetComponent<Stats>().health.ChangeValue(-damage);
        }

        if (collider.gameObject.GetComponent<Stats>() != null && killOnExit)
        {
            if (resetPoint != null)
            {
                collider.gameObject.GetComponent<Stats>().resetPoint = resetPoint;
            }

            collider.gameObject.GetComponent<Stats>().Die();
        }
    }    
}
