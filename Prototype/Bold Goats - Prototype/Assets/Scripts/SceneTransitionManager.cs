using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void LoadScene(string _sceneName)
    {
        SceneManager.LoadScene(_sceneName);
    }
    public void LoadSceneAsync(string _sceneName)
    {
        LoadSceneEnumerator(_sceneName);
    }

    IEnumerator LoadSceneEnumerator(string _sceneName)
    {
     
        AsyncOperation unload = SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        AsyncOperation load = SceneManager.LoadSceneAsync(_sceneName);
        while (!unload.isDone || !load.isDone)
        {
            yield return null;
        }      
    }
    IEnumerator LoadSceneEnumerator(int _buildIndex)
    {

        AsyncOperation unload = SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        AsyncOperation load = SceneManager.LoadSceneAsync(_buildIndex);
        while (!unload.isDone || !load.isDone)
        {
            yield return null;
        }

    }

    public void LoadScene(int _buildIndex)
    {
        SceneManager.LoadScene(_buildIndex);
    }
   
    public void LoadSceneAsync(int _buildIndex)
    {
        StartCoroutine(LoadSceneEnumerator(_buildIndex));
    }

    public void SetCursor(bool condition)
    {
        Cursor.visible = condition;
    }
}
