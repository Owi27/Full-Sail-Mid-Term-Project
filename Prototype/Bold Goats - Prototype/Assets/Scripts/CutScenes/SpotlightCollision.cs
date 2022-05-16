using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotlightCollision : MonoBehaviour
{
    public GameObject SpotlightCam;
    public GameObject PlayerCam;
    public GameObject skipText;
    private float initWalking = 0;
    private float initRunning = 0;


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
        initWalking = GameManager.Instance.Player.GetComponent<PlayerController>().WalkSpeed;
        initRunning = GameManager.Instance.Player.GetComponent<PlayerController>().RunSpeed;
        GameManager.Instance.Player.GetComponent<PlayerController>().WalkSpeed = 0f;
        GameManager.Instance.Player.GetComponent<PlayerController>().RunSpeed = 0f;
        skipText.SetActive(true);
        SpotlightCam.SetActive(true);
        PlayerCam.SetActive(false);
        GameManager.Instance.Player.GetComponentInChildren<Animator>().enabled = false;
        yield return new WaitForSeconds(8);
        PlayerCam.SetActive(true);
        SpotlightCam.SetActive(false);
        skipText.SetActive(false);
        Shader.SetGlobalFloat("_GlobalPlayerVisibility", 1f);
        GameManager.Instance.Player.GetComponent<PlayerController>().WalkSpeed = initWalking;
        GameManager.Instance.Player.GetComponent<PlayerController>().RunSpeed = initRunning;
        GameManager.Instance.Player.GetComponentInChildren<Animator>().enabled = true;
    }

    void SkipScene()
    {
        StopAllCoroutines();
        PlayerCam.SetActive(true);
        SpotlightCam.SetActive(false);
        skipText.SetActive(false);
        GameManager.Instance.Player.GetComponentInChildren<Animator>().enabled = true;

    }
}
