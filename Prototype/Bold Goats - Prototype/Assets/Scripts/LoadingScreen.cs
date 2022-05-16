using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LoadingScreen : MonoBehaviour
{

    public Slider slider;
   
    void Start()
    {
        slider.value = 0;
        StartCoroutine(Load());
    }
    IEnumerator Load()
    {
        AsyncOperation load = SceneManager.LoadSceneAsync("Palace");

        while (!load.isDone)
        {
            slider.value = load.progress;
            yield return null;
        }
    }
}
