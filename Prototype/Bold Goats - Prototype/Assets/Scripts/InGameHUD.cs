using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameHUD : MonoBehaviour
{
    public Image amountOfDistractables;
    public Sprite[] xAmountOfDistractables = new Sprite[4];
    public Slider staminaBar;
    public Slider xrayBar;
    private void Start()
    {
        staminaBar.value = PlayerController.Stamina;
        xrayBar.value = PlayerController.xraytime * 2;
    }

    // Update is called once per frame
    void Update()
    {
        amountOfDistractables.sprite = xAmountOfDistractables[PlayerController.AmountOfDistractables];

        staminaBar.value = PlayerController.Stamina;
        xrayBar.value = PlayerController.xraytime / PlayerController.xrayTimeLimit;
    }
}
