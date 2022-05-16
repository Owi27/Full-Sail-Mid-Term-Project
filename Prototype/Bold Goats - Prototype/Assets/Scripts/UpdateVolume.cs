using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UpdateVolume : MonoBehaviour
{
    public void UpdateMixer(AudioMixer mixer)
    {
        mixer.SetFloat("Volume", GetComponent<Slider>().value);
    }
}
