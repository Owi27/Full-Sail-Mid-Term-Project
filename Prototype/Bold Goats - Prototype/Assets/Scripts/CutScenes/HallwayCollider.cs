using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallwayCollider : MonoBehaviour
{
    public GameObject HallwayCam;
    public GameObject PlayerCam;
    public GameObject skipText;
    private float initWalkSpeed = 0;
    private float initRunSpeed = 0;

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
        initWalkSpeed = GameManager.Instance.Player.GetComponent<PlayerController>().WalkSpeed;
        initRunSpeed = GameManager.Instance.Player.GetComponent<PlayerController>().RunSpeed;
        GameManager.Instance.Player.GetComponent<PlayerController>().WalkSpeed = 0f;
        GameManager.Instance.Player.GetComponent<PlayerController>().RunSpeed = 0f;
        GameManager.Instance.Player.GetComponentInChildren<Animator>().enabled = false;
        skipText.SetActive(true);
        HallwayCam.SetActive(true);
        PlayerCam.SetActive(false);      
        yield return new WaitForSeconds(8);
        PlayerCam.SetActive(true);
        HallwayCam.SetActive(false);
        skipText.SetActive(false);
        Shader.SetGlobalFloat("_GlobalPlayerVisibility", 1f);
        GameManager.Instance.Player.GetComponent<PlayerController>().WalkSpeed = initWalkSpeed;
        GameManager.Instance.Player.GetComponent<PlayerController>().RunSpeed = initRunSpeed;
        GameManager.Instance.Player.GetComponentInChildren<Animator>().enabled = true;
    }

    void SkipScene()
    {
        StopAllCoroutines();
        PlayerCam.SetActive(true);
        HallwayCam.SetActive(false);
        skipText.SetActive(false);
        Shader.SetGlobalFloat("_GlobalPlayerVisibility", 1f);
        GameManager.Instance.Player.GetComponent<PlayerController>().WalkSpeed = initWalkSpeed;
        GameManager.Instance.Player.GetComponent<PlayerController>().RunSpeed = initRunSpeed;
        GameManager.Instance.Player.GetComponentInChildren<Animator>().enabled = true;

    }
}
