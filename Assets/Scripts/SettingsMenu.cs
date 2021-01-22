using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
/**
* @author Marvin Rausch
*/
public class SettingsMenu : MonoBehaviour
{
    public static float vibrationDuration = 100;

    private Slider _slider;
    private Toggle _textPositionToggle;
    public Slider sliderUI;
    public TextMeshProUGUI textSliderValue;
    
    private void Start()
    {
        _slider = gameObject.GetComponentInChildren<Slider>();
        
        _textPositionToggle = gameObject.GetComponentInChildren<Toggle>();
        _textPositionToggle.isOn = UIScript.useWorldSpaceTextfield;

        vibrationDuration = _slider.value;
        ShowSliderValue();
    }
    
    /// <summary>
    /// set vibration time
    /// </summary>
    /// <param name="duration"></param> duration time of vibration in milliseconds
    public void SetDuration(float duration)
    {
        // set duration of vibration when target is recognized
        vibrationDuration = duration;
    }
    
    /// <summary>
    /// switch between 2D description text and world space canvas mode
    /// </summary>
    /// <param name="change"></param>
    public void ToggleValueChanged(Toggle change)
    {
        UIScript.useWorldSpaceTextfield = change.isOn;
    }
    
    /// <summary>
    /// show current value of vibration time
    /// </summary>
    public void ShowSliderValue () {
        string sliderMessage =   Math.Round(sliderUI.value, 0) +  "ms";
        textSliderValue.text = sliderMessage;
    }
}
