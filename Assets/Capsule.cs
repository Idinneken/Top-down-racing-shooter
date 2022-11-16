using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Capsule : MonoBehaviour
{
    public Vector3 localPosition;
    internal Vector3 position;
    public float radius;
    public Color color = Color.white;
    [Space]
    public bool wireFrame;

    //private void Update()
    //{
    //    position = transform.position + localPosition;
    //}

    //void OnDrawGizmosSelected()
    //{
    //    position = transform.position + localPosition;
    //    Gizmos.color = color;

    //    if (wireFrame)
    //    {                        
    //        Gizmos.DrawMesh(position, radius);
    //    }
    //    else
    //    {
    //        Gizmos.DrawSphere(position, radius);
    //    }
    //}

    //public void ChangeColour(Color color_)
    //{
    //    color = color_;
    //}
}

