using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGateKill : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") 
        {
            

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            StartCoroutine(SpotPlayer());

        }
    }

    IEnumerator SpotPlayer()
    {
        var player = GameManager.Instance.Player.GetComponent<PlayerController>();
        player.WalkSpeed = 0;
        player.RunSpeed = 0;
        SoundManager.PlaySound(SoundManager.Sound.SpotlightFindsPlayer);
        yield return new WaitForSecondsRealtime(.2f);
        SceneTransitionManager.Instance.LoadScene("LOSE CONDITION");
    }
}
