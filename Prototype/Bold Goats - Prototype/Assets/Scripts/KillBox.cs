using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class KillBox : MonoBehaviour
{
    public GameObject text;
    void OnTriggerEnter(Collider collider) 
    {
        if (collider.tag == "Player") 
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SoundManager.PlaySound(SoundManager.Sound.PlayerWaterDeath);
            
            if (text != null)
            {
                text.SetActive(true);
            }
            StartCoroutine(WaitSomeTime(2f));
            
        }
    }
    
    IEnumerator WaitSomeTime(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        SceneTransitionManager.Instance.LoadScene("LOSE_CONDITION");
    }
       
    
}
