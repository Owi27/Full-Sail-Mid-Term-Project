using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    

    public void Retry()
    {
        SceneManager.LoadScene("Palace");
    }
    public void Quit()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
