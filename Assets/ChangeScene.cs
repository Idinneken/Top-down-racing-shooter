using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string sceneName;
    
    public void ChangeToScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ChangeToScene(string sceneName_)
    {
        SceneManager.LoadScene(sceneName_);
    }
}
