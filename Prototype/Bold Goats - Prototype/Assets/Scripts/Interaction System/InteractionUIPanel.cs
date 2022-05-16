using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractionUIPanel : MonoBehaviour
{
    [SerializeField] private Image progressBar;
    [SerializeField] private TextMeshProUGUI tooltipText;
    [SerializeField] private GameObject pressKey;
    [SerializeField] private GameObject bar;

    public void Show(bool on)
    {
        if (on == true)
        {
            pressKey.SetActive(true);
            bar.SetActive(true);
        }
        else
        {
            pressKey.SetActive(false);
            bar.SetActive(false);
        }
    }

    public void SetTooltip(string _tooltip)
    {
        tooltipText.SetText(_tooltip);
    }

    public void UpdateProgressBar(float _fillamount)
    {
        progressBar.fillAmount = _fillamount;
    }

    public void ResetUI()
    {
        progressBar.fillAmount = 0f;
        tooltipText.SetText("");
    }
}
