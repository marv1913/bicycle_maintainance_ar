using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public static float vibrationDuration;

    private Slider _slider;

    private void Start()
    {
        _slider = gameObject.GetComponentInChildren<Slider>();
        vibrationDuration = _slider.value;
    }

    public void SetDuration(float duration)
    {
        // set duration of vibration when target is recognized
        vibrationDuration = duration;
    }
  
}
