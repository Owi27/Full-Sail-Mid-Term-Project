using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayTextOnCollision : MonoBehaviour
{

    public GameObject textToDisplay;

    private void OnTriggerEnter(Collider other)
    {
        textToDisplay.SetActive(true);
        StartCoroutine(WaitSomeTime(5));

    }

    IEnumerator WaitSomeTime(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        textToDisplay.SetActive(false);
        Destroy(this);
    }
}
