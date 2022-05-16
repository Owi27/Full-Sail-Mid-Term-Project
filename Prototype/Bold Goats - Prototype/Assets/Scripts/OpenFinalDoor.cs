using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenFinalDoor : MonoBehaviour
{
    public GameObject[] doorsToOpen;

    public GameObject textToDisplay;
    public int timeToDisplayText;

    public GameObject door1;
    public GameObject door2;

    //private void Update()
    //{
    //    if (GameManager.Instance.keyCards >= 3)
    //    {
    //        Destroy(door2);
    //        Destroy(door1);
    //    }
    //}


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (GameManager.Instance.keyCards >= 3)
            {
                //Open door
                foreach (var door in doorsToOpen)
                {
                    Destroy(door);
                    //door.GetComponent<Animator>().SetTrigger("Open");
                }

            } 
            else
            {
                //display text saying to get all 3 keys

                textToDisplay.SetActive(true);

                float time = Time.time + timeToDisplayText;

                //while (time >= Time.time)
                //{
                //    continue;
                //}

                textToDisplay.SetActive(false);
            }
        }
    }


}
