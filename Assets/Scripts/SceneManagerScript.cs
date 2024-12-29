using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public void SceneChange(string scenename) 
    { 
        SceneManager.LoadScene(scenename);
    }
}
