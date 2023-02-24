using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public KeyCode keybind;
    public GameObject pauseMenu; 

    private bool active = false;

    void Update()
    {
        if (Input.GetKeyDown(keybind))
        {
            TogglePauseMenu();
        }
    }

    public void TogglePauseMenu()
    {
        active = !active;
        SetPauseMenuActive(active);
    }    

    public void SetPauseMenuActive(bool active_)
    {
        pauseMenu.SetActive(active_);
        SetCursorActive(active_);

        if(!active_)
        {
            GetComponent<Timescale>().SetTimescale(1f);
        }
        else
        {
            GetComponent<Timescale>().SetTimescale(0f);

        }
    }

    public void SetCursorActive(bool active_)
    {
        Cursor.visible = active_;
    }
}
