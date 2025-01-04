using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public void SceneChange(string scenename) 
    { 
        SceneManager.LoadScene(scenename);
    }

    public void SceneRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitApp()
    {
        Application.Quit();
    }
}
