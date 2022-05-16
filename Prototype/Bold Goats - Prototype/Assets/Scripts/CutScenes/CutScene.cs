using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CutScene : MonoBehaviour
{
    public GameObject PlayerCamera;
    public GameObject HallwayCam;
    public GameObject SpotlightCam;
    public GameObject LazerMazeCam;
    public GameObject MazeCam;
    public GameObject WinRoomCam;

    void Start()
    {
        StartCoroutine(CutScenes());
    }
    IEnumerator CutScenes()
    {
        HallwayCam.SetActive(true);
        yield return new WaitForSeconds(12);
        SpotlightCam.SetActive(true);
        HallwayCam.SetActive(false);
        yield return new WaitForSeconds(7);
        LazerMazeCam.SetActive(true);
        SpotlightCam.SetActive(false);
        yield return new WaitForSeconds(7);
        MazeCam.SetActive(true);
        LazerMazeCam.SetActive(false);
        yield return new WaitForSeconds(6);
        WinRoomCam.SetActive(true);
        MazeCam.SetActive(false);
        yield return new WaitForSeconds(9.5f);
        WinRoomCam.SetActive(false);
    }
}