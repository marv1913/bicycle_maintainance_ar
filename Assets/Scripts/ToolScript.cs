using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolScript : MonoBehaviour
{
    public string savefileName;
    public UIScript uiScript;
    public List<ToolMovement> toolMovements;
    public long sleepTimeBetweenVibrate = 100;
    public GameObject canvasPosition;



    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    /// <summary>
    /// should be added to OnTargetFoundEvent
    /// </summary>
    public void ToolDetected()
    {
        Debug.Log("ToolScript has detected tool");
        RefreshUI();
        uiScript.toolScript = this;
        uiScript.EnableUI();
        uiScript.worldSpaceDescriptionText.SetReferenceObject(canvasPosition);
        Vibration.VibrateTwice(Convert.ToInt64(SettingsMenu.vibrationDuration * 1000) / 1000, sleepTimeBetweenVibrate);
    }

    public void ToolLost()
    {
        uiScript.DisableUI();
    }

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