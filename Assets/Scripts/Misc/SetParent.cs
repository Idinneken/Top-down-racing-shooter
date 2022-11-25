using UnityEditor;
using UnityEngine;

public class SetParent : MonoBehaviour
{
    public string parentName;
    public bool set = true;

    private void Reset()
    {
        if (!string.IsNullOrEmpty(parentName) && !string.IsNullOrWhiteSpace(parentName))
        {
            SetParentDirty();
        }
    }


#if UNITY_EDITOR
    private void OnValidate() => EditorApplication.delayCall += _OnValidate;

    private void _OnValidate()
    {
        EditorApplication.delayCall -= _OnValidate;
        if (this == null) return;
        if (!set)
        {
            if (!string.IsNullOrEmpty(parentName) && !string.IsNullOrWhiteSpace(parentName))
            {
                SetParentDirty();
            }
        }
    }
#endif    

    private void SetParentDirty()
    {
        GameObject parent = GameObject.Find(parentName) ?? new GameObject(parentName);

        EditorUtility.SetDirty(gameObject);
        transform.SetParent(parent.transform);
        set = true;
        EditorUtility.ClearDirty(gameObject);
    }
}
