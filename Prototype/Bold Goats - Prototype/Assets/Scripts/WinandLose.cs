using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinandLose : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneTransitionManager.Instance.LoadScene("LOSE CONDITION");
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            SceneTransitionManager.Instance.LoadScene("WINCONDITION");
        }
    }
    public void HandleButton(string name)
    {
        SceneTransitionManager.Instance.LoadScene(name);
    }
}
