using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableBehaviour : InteractableBase
{
    [Space]
    public bool destroyType = false;
    public GameObject destroy;

    [Space]
    public bool hideType = false;
    //public Transform hidePoint;

    [Space]
    public bool transportType = false;
    public Transform transportPoint;

    [Space]
    public GameObject textToDisplay;

    public float timeToDisplayText;

    public InteractionUIPanel uiPanel;
    GameObject holdPanel;

    [Space]
    public GameObject cutsceneCam;
    public GameObject cameraToTurnOff;
    public float cutsceneLength;

    private void Start()
    {
        //uiPanel = GameObject.Find("InteractionUIPanel");
        holdPanel = GameObject.Find("InteractionUIPanel");
        uiPanel = holdPanel.GetComponent<InteractionUIPanel>();
    }

    public override void OnInteract()
    {
        base.OnInteract();

        if (destroyType == true)
        {
            if ((textToDisplay != null && destroy != null) || destroy != null)
            {
                StartCoroutine(WaitCompletionText());
            }
            if (destroy != null)
            {
                //Destroy(destroy);

            }

        }
        else if (transportType == true)
        {
            GameManager.Instance.Player.transform.position = transportPoint.position;
        }
        else if (hideType == true)
        {
            GameManager.Instance.Player.transform.position = transform.position;
            GetComponent<Collider>().enabled = false;

            if (Vector3.Distance(GameManager.Instance.Player.transform.position, transform.position) > .03f)
            {
                GetComponent<Collider>().enabled = true;
            }
        }
        SoundManager.PlaySound(SoundManager.Sound.DoorOpen);
        GameManager.Instance.keyCards++;
        GameManager.Instance.KeyCardCountText.text = GameManager.Instance.keyCards.ToString();
    }

    IEnumerator WaitCompletionText()
    {
        holdPanel.SetActive(false);

        if (textToDisplay != null)
        {
            textToDisplay.SetActive(true);
            yield return new WaitForSecondsRealtime(timeToDisplayText);
            textToDisplay.SetActive(false);
        }
        if (cutsceneCam != null)
        {
            StartCoroutine(CameraCutscenes());
        }
        else
        {
            Destroy(destroy);
        }
    }

    IEnumerator CameraCutscenes()
    {
        cutsceneCam.SetActive(true);
        cameraToTurnOff.SetActive(false);
        yield return new WaitForSeconds(cutsceneLength);
        cameraToTurnOff.SetActive(true);
        cutsceneCam.SetActive(false);
        holdPanel.SetActive(true);
        Destroy(destroy);
    }

}