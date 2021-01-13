using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public static float vibrationDuration = 100;

    private Slider _slider;
    private Toggle _textPositionToggle;

    private void Start()
    {
        _slider = gameObject.GetComponentInChildren<Slider>();
        
        _textPositionToggle = gameObject.GetComponentInChildren<Toggle>();
        _textPositionToggle.isOn = UIScript.useWorldSpaceTextfield;

        vibrationDuration = _slider.value;
    }

    public void SetDuration(float duration)
    {
        // set duration of vibration when target is recognized
        vibrationDuration = duration;
    }
    
    public void ToggleValueChanged(Toggle change)
    {
        UIScript.useWorldSpaceTextfield = change.isOn;
    }
  
}
