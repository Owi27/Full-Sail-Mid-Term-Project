using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphicsSettings : MonoBehaviour
{
    Resolution[] resolutions;
    public TMPro.TMP_Dropdown resDropdown;

    public void ToggleFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
    void Start()
    {
        resolutions = Screen.resolutions;
        resDropdown.ClearOptions();
        List<string> res = new List<string>();
        int currentRes = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string tempRes = resolutions[i].width + " x " + resolutions[i].height + " : " + resolutions[i].refreshRate;
            res.Add(tempRes);
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentRes = i;
            }
        }
        resDropdown.AddOptions(res);
        resDropdown.value = currentRes;
        resDropdown.RefreshShownValue();
    }
    public void SetRes(int _resIndex)
    {
        if (resolutions !=null)
        {
            Resolution resolution = resolutions[_resIndex];

            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        }
    }
}
