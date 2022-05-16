using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenDoors : MonoBehaviour
{
    public GameObject[] doorsToOpen;

    public Text textToDisplay;
    public int timeToDisplayText;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            if (GameManager.Instance.keyCards >= 3)
            {
                //Open door
                foreach (var door in doorsToOpen)
                {
                    Debug.Log(door);
                    door.GetComponent<Animator>().SetTrigger("Open");
                }

            }
            else
            {
                //display text saying to get all 3 keys

                textToDisplay.enabled = true;

                float time = Time.time + timeToDisplayText;

                while (time >= Time.time)
                {
                    continue;
                }

                textToDisplay.enabled = false;
            }
        }
    }
}
