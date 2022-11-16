using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Sphere : MonoBehaviour
{    
    public float radius;
    public Color color = Color.white;
    [Space]
    public bool wireFrame;    

    void OnDrawGizmosSelected()
    {
        
        Gizmos.color = color;

        if (wireFrame)
        {
            Gizmos.DrawWireSphere(transform.position, radius);

        }
        else
        {
            Gizmos.DrawSphere(transform.position, radius);
        }
    }

    public void ChangeColour(Color color_)
    {
        color = color_;
    }
}

