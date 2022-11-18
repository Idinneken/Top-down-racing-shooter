using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawShape : MonoBehaviour
{
    public MeshFilter mesh;
    public Color color = Color.white;
    [Space]
    public bool alwaysDraw = true, solidOnSelect = false;

    void OnDrawGizmos()
    {   
        if (alwaysDraw && TryGetComponent(out mesh))
        {
            Gizmos.color = color;            
            Gizmos.DrawWireMesh(mesh.sharedMesh, transform.position, transform.rotation, transform.localScale);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (solidOnSelect && TryGetComponent(out mesh))
        {
            Gizmos.color = color;
            Gizmos.DrawWireMesh(mesh.sharedMesh, transform.position, transform.rotation, transform.localScale);        
        }
        else if (!solidOnSelect && TryGetComponent(out mesh))
        {
            Gizmos.color = color;
            Gizmos.DrawMesh(mesh.sharedMesh, transform.position, transform.rotation, transform.localScale); 
        }
    }

}
