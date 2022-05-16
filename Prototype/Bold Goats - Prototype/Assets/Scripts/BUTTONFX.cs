using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BUTTONFX : MonoBehaviour
{
    public AudioSource FX;
    public AudioClip Hoverfx;
    public AudioClip Clickfx;

    public void HoverSound()
    {
        //FX.PlayOneShot(Hoverfx);
        SoundManager.PlaySound(SoundManager.Sound.HoverMenuItem);
    }
    public void ClickSound()
    {
        //FX.PlayOneShot(Clickfx);
        SoundManager.PlaySound(SoundManager.Sound.ClickMenuItem);
    }
}
