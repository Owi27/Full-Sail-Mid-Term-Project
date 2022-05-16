using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinRoomCollider : MonoBehaviour
{
    public GameObject WinRoomCam;
    public GameObject PlayerCam;
    public GameObject skipText;
    private float initalWalking = 0;
    private float initalRunning = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            SkipScene();
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            this.gameObject.GetComponent<BoxCollider>().enabled = false;

            StartCoroutine(FinishCutScene());
        }
    }

    IEnumerator FinishCutScene()
    {
        Shader.SetGlobalFloat("_GlobalPlayerVisibility", 0f);
        initalWalking = GameManager.Instance.Player.GetComponent<PlayerController>().WalkSpeed;
        initalRunning = GameManager.Instance.Player.GetComponent<PlayerController>().RunSpeed;
        GameManager.Instance.Player.GetComponent<PlayerController>().WalkSpeed = 0f;
        GameManager.Instance.Player.GetComponent<PlayerController>().RunSpeed = 0f;
        skipText.SetActive(true);
        WinRoomCam.SetActive(true);
        PlayerCam.SetActive(false);
        GameManager.Instance.Player.GetComponentInChildren<Animator>().enabled = false;
        yield return new WaitForSeconds(8);
        PlayerCam.SetActive(true);
        WinRoomCam.SetActive(false);
        skipText.SetActive(false);
        Shader.SetGlobalFloat("_GlobalPlayerVisibility", 1f);
        GameManager.Instance.Player.GetComponent<PlayerController>().WalkSpeed = initalWalking;
        GameManager.Instance.Player.GetComponent<PlayerController>().RunSpeed = initalRunning;
        GameManager.Instance.Player.GetComponentInChildren<Animator>().enabled = true;
    }

    void SkipScene()
    {
        StopAllCoroutines();
        PlayerCam.SetActive(true);
        WinRoomCam.SetActive(false);
        skipText.SetActive(false);
        GameManager.Instance.Player.GetComponentInChildren<Animator>().enabled = true;

    }
}
