using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string sceneName;
    
    public void ChangeToScene()
    {
        if (!string.IsNullOrWhiteSpace(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            print(sceneName + " doesn't exist");
        }
    }

    public void ChangeToScene(string sceneName_)
    {
        if (!string.IsNullOrWhiteSpace(sceneName_))
        {
            SceneManager.LoadScene(sceneName_);
        }
        else
        {
            print(sceneName_ + " doesn't exist");
        }
    }
}
