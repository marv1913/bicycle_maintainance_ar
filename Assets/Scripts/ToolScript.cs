using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
* @author Marvin Rausch
*/
public class ToolScript : MonoBehaviour
{
    public string savefileName;
    public UIScript uiScript;
    public List<ToolMovement> toolMovements;
    public long sleepTimeBetweenVibrate = 100;
    public GameObject canvasPosition;
    
    public void ToolDetected()
    {
        Debug.Log("ToolScript has detected tool");
        Vibration.VibrateTwice(Convert.ToInt64(SettingsMenu.vibrationDuration * 1000) / 1000, sleepTimeBetweenVibrate);

        RefreshUI();
        uiScript.toolScript = this;
        uiScript.EnableUI();
        uiScript.worldSpaceDescriptionText.SetReferenceObject(canvasPosition);
    }
    
    
    /// <summary>
    /// start the movement of a tool
    /// </summary>
    /// <param name="index"></param>
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

    public void RefreshUI()
    {
        // load button names and description text for detected Tool
        uiScript.LoadAppropriateUIText(savefileName);
    }
}