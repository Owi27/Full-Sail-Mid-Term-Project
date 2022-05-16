using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WinConTrigger : MonoBehaviour
{
    public GameObject WinScreenUI;
    public GameObject HudtoHide;

    public GameObject CameratoHide;
    public GameObject WinRoomCam;

    public GameObject Music;
    public GameObject MusictoHide;

    public GameObject soundeffect;
    
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("COLLIDED");
        soundeffect.SetActive(true);
        HudtoHide.SetActive(false);
        WinScreenUI.SetActive(true);
        CameratoHide.SetActive(false);
        WinRoomCam.SetActive(true);
        //MusictoHide.SetActive(false);
        //Music.SetActive(true);
        
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
