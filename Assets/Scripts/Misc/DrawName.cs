using UnityEngine;
using UnityEditor;

public class DrawName : MonoBehaviour
{
    public string editorName;
    public Vector3 labelOffset;

    public GUIStyle style;

    void OnDrawGizmos()
    {
        if (string.IsNullOrWhiteSpace(editorName))
        {            
            Handles.Label(transform.position + labelOffset, gameObject.name, style);
        }
        else
        {
            Handles.Label(transform.position + labelOffset, editorName, style);
        }

    }
    
    void OnDrawGizmosSelected()
    {
        if (string.IsNullOrWhiteSpace(editorName))
        {
            Handles.Label(transform.position + labelOffset, gameObject.name, style);
        }
        else
        {
            Handles.Label(transform.position + labelOffset, editorName, style);
        }

    }
}
