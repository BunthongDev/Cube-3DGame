using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void LoadScene(string sceneName)
    {
        Debug.Log("load scene : " + sceneName);
        SceneManager.LoadScene(sceneName);
    }
}
