using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCon : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        SceneTransitionManager.Instance.LoadScene("WINCONDITION");
    }
}
