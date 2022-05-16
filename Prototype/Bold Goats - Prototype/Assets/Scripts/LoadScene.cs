using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadScene : MonoBehaviour
{
   public void LoadSceneButton(string scen_name)
    {
        SceneManager.LoadScene(scen_name);
    }
    
    public void ToggleCursor(bool condition)
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
