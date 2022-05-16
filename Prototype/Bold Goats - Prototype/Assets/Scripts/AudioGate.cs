using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioGate : MonoBehaviour
{
    // Start is called before the first frame update
    bool entered = false;
    public AudioSource audioS;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") 
        {


            entered = !entered;

            if (entered == true)
            {
                audioS.Play();
                GameObject.Find("BG SONG").GetComponent<AudioSource>().Stop();
            }
            else 
            {
                audioS.Stop();
                GameObject.Find("BG SONG").GetComponent<AudioSource>().Play();
            }
        }
    }
}
