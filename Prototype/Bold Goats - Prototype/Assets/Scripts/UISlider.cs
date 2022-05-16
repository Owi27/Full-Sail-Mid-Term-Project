using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISlider : MonoBehaviour
{
    public Slider SoundSlider;
    public Slider MusicSlider;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("MusicSlider"))
        {
            MusicSlider.value = PlayerPrefs.GetFloat("MusicSlider");
        } else
        {
            MusicSlider.value = 0;
        }
        if (PlayerPrefs.HasKey("SoundSlider"))
        {
            SoundSlider.value = PlayerPrefs.GetFloat("MusicSlider");
        }
        else
        {
            SoundSlider.value = 0;
        }

        
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat("MusicSlider", MusicSlider.value);
        PlayerPrefs.SetFloat("SoundSlider", SoundSlider.value);
    }
}
