using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(string _sceneName)
    {
        SceneManager.LoadScene(_sceneName);
    }
    public void LoadScene(int _sceneNum)
    {
        SceneManager.LoadScene(_sceneNum);
    }
}
