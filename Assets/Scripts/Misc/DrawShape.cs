#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

#if UNITY_EDITOR
public class DrawShape : MonoBehaviour
{
    public MeshFilter mesh;
    public Color selectedColor = new Color(0.75f, 0.75f, 0.75f, 0.75f);
    public Color unSelectedColor = new Color(0.25f,0.25f,0.25f,0.125f);
    [Space]

    public bool draw = true;
    public bool alwaysDraw = true;
    public bool solidOnSelect = false;

    void OnDrawGizmos()
    {   
        if (draw && alwaysDraw && TryGetComponent(out mesh) && !Selection.Contains(gameObject))
        {
            Gizmos.color = unSelectedColor;            
            Gizmos.DrawWireMesh(mesh.sharedMesh, transform.position, transform.rotation, transform.localScale);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = selectedColor;

        if (draw && solidOnSelect && TryGetComponent(out mesh))
        {
            Gizmos.DrawMesh(mesh.sharedMesh, transform.position, transform.rotation, transform.localScale); 
        }
        else if (draw && !solidOnSelect && TryGetComponent(out mesh))
        {            
            Gizmos.DrawWireMesh(mesh.sharedMesh, transform.position, transform.rotation, transform.localScale);        
        }
    }

}
#endif
