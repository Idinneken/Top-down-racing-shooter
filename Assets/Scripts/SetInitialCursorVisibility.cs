using UnityEngine;

public class SetInitialCursorVisibility : MonoBehaviour
{
    public bool startVisible = false;
    
    void Start()
    {
        Cursor.visible = startVisible;       
    }

}
