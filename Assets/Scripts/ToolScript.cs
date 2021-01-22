using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
* @author Marvin Rausch
* script for all repair tools to manage what should happen when a tool was detected
*/
public class ToolScript : MonoBehaviour
{
    public string savefileName;
    public UIScript uiScript;
    public List<ToolMovement> toolMovements;
    public long sleepTimeBetweenVibrate = 100;
    public GameObject canvasPosition;
    
    /// <summary>
    /// give user haptically feedback and refresh UI
    /// </summary>
    public void ToolDetected()
    {
        Debug.Log("ToolScript has detected tool");
        Vibration.VibrateTwice(Convert.ToInt64(SettingsMenu.vibrationDuration * 1000) / 1000, sleepTimeBetweenVibrate);

        RefreshUI();
        uiScript.toolScript = this;
        uiScript.FillDropdownMenu();
        uiScript.worldSpaceDescriptionText.SetReferenceObject(canvasPosition);
    }
    
    /// <summary>
    /// start the movement of the correct tool
    /// </summary>
    /// <param name="index"></param> index of tool in list which should be set active
    public void StartAnimation(int index)
    {
        Debug.Log("starting animation");
        foreach (var toolMovement in toolMovements)
        {
            if (null != toolMovement)
            {
                toolMovement.StopMovement();
            }
        }

        if (null != toolMovements[index])
        {
            toolMovements[index].StartMovement();
        }
    }
    
    /// <summary>
    /// load instruction text for tool
    /// </summary>
    public void RefreshUI()
    {
        // load button names and description text for detected Tool
        uiScript.LoadAppropriateUIText(savefileName);
    }
}