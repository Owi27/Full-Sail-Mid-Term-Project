using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGateSwitch : MonoBehaviour
{
    public bool IsOn = true;
    public bool IsPermanentlyOn = false;
    public float SwitchTime = 6f;
    public float IndicatorSwitchTime = 1f;
    public GameObject Lasers;
    public Material Indicator;
    

    
    // Start is called before the first frame update
    void Start()
    {
        Lasers.SetActive(IsOn);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPermanentlyOn == false) 
        {
            SwitchTime -= Time.deltaTime;

            if (SwitchTime <= 3f && IsOn == false)
            {
                IndicatorSwitchTime -= Time.deltaTime;
                if (IndicatorSwitchTime <= 0f)
                {
                    Indicator.SetColor("_Color", Color.red);
                    IndicatorSwitchTime = 1f;
                }
                else
                {
                    Indicator.SetColor("_Color", Color.white);
                }
            }
            else if (IsOn == false)
            {
                Indicator.SetColor("_Color", Color.white);
            }
            else if (IsOn == true) 
            {
                Indicator.SetColor("_Color", Color.red);
            }
             
            if (SwitchTime < 0.1)
            {
                SwitchTime = 6.0f;
                IsOn = !IsOn;
                Lasers.SetActive(IsOn);
                IndicatorSwitchTime = 1f;
            }
            
        }
    }

  
}
