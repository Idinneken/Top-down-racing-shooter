//using System.Collections;
//using System.Collections.Generic;
//using UnityEditor;
//using UnityEngine;

//public class Shape : MonoBehaviour
//{
//    public Vector3 localPosition;
//    internal new Transform transform;
//    public float radius;
//    public Color color = Color.white;
//    [Space]
//    public bool wireFrame;

//    private void Update()
//    {
//        transform.position = transform.position + localPosition;
//        transform.parent = gameObject.transform;
        
//    }

//    void OnDrawGizmosSelected()
//    {
//        transform.position = transform.position + localPosition;
//        Gizmos.color = color;

//        if (wireFrame)
//        {            
//            Gizmos.DrawWireSphere(transform.position, radius);            

//        }
//        else
//        {
//            Gizmos.DrawSphere(transform.position, radius);
//        }
//    }

//    public void ChangeColour(Color color_)
//    {
//        color = color_;
//    }
//}

