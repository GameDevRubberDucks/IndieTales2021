using UnityEngine;
using UnityEngine.SceneManagement;

public class Util_SceneLoader : MonoBehaviour
{
    //--- Public Methods ---//
    public void LoadSceneByName(string _sceneName)
    {
        SceneManager.LoadScene(_sceneName);
    }

    public void LoadSceneByIndex(int _sceneIndex)
    {
        SceneManager.LoadScene(_sceneIndex);
    }

    public void ReloadCurrentScene()
    {
        LoadSceneByName(SceneManager.GetActiveScene().name);
    }
}
