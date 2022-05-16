using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject uiForPause;

    private void Awake()
    {
        Screen.fullScreen = true;

    }

    void Start()
    {
        resolutions = Screen.resolutions;
        resDropdown.ClearOptions();
        List<string> res = new List<string>();
        int currentRes = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].width >= 854 && resolutions[i].height >= 480)
            {
                string tempRes = resolutions[i].width + " x " + resolutions[i].height + " : " + resolutions[i].refreshRate;
                res.Add(tempRes);
            }
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentRes = i;
            }
        }

        resDropdown.AddOptions(res);
        resDropdown.value = currentRes;
        resDropdown.RefreshShownValue();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && uiForPause != null)
        {
            if (GameManager.Instance.isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }

    public void Resume()
    {
        Camera.main.GetComponent<CameraMovement>().enabled = true;
        SoundManager.PlaySound(SoundManager.Sound.UnpauseGame);
        uiForPause.SetActive(false);
        Cursor.visible = false;
        Time.timeScale = 1f;
        GameManager.Instance.isPaused = false;
    }

    void Pause()
    {
        GameManager.Instance.Player.GetComponent<PlayerController>().UnXray();
        if (Camera.main != null) Camera.main.GetComponent<CameraMovement>().enabled = false;
        SoundManager.PlaySound(SoundManager.Sound.PauseGame);
        uiForPause.SetActive(true);
        Time.timeScale = 0f;
        GameManager.Instance.isPaused = true;
    }

    public void Options()
    {

    }

    public void Save()
    {

    }

    public void LoadSave()
    {

    }

    public void Quit()
    {
        SceneManager.LoadScene("Main Menu");
    }

    // Graphic Settings

    Resolution[] resolutions;
    public TMPro.TMP_Dropdown resDropdown;

    public void ToggleFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }


    public void SetRes(int _resIndex)
    {
        Resolution resolution = resolutions[_resIndex];

        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    // Sound Settings
    public AudioMixer musicMixer;
    public AudioMixer sfxMixer;

    public void SetMusicVolume(float _volume)
    {
        musicMixer.SetFloat("MusicVolume", _volume);
    }

    public void SetSFXVolume(float _volume)
    {
        sfxMixer.SetFloat("SFXVolume", _volume);
    }

}
