using System.Collections;
using System.Collections.Generic;

using TMPro;
using UnityEngine;
using UnityEngine.UI;

/**
* @author Marvin Rausch
*/
public class MaintenanceScript : MonoBehaviour
{
    public TextMeshProUGUI text;
    public List<ToolMovement> tools;
    public Button previousButton;
    public GameObject overlayMenu;
    public GameObject maintenanceFinishedMenu;
    
    private int _index = 0;
    private UIScript.Guidelines _guidelines;
    void Start()
    {
        // make sure there is no tool active on beginning
        foreach (var tool in tools)
        {
            tool.StopMovement();
        }
        // load instructions from JSON
        _guidelines = UIScript.LoadGuidelines(MaintenanceMenu.componentToMaintain);
        List<UIScript.Guideline> temp = _guidelines.guidelines;
        // delete first element, because the introduction text is not a component
        temp.RemoveAt(0);
        _guidelines.guidelines = temp;
        // load first instruction and it's animation
        LoadToolAndText();
        SetVisibilityOfPreviousButton();
    }
    
    /// <summary>
    /// load description text of current maintenance step
    /// </summary>
    private void LoadText()
    {
        text.text = _guidelines.guidelines[_index].descriptionText;
    }
    
    /// <summary>
    /// load next step
    /// </summary>
    public void ShowNextStep()
    {
        LoadStep(true);
    }
    
    /// <summary>
    /// load previous step
    /// </summary>
    public void ShowPreviousStep()
    {
        LoadStep(false);
    }
    
    /// <summary>
    /// load next or previous maintenance step
    /// </summary>
    /// <param name="nextStep"></param> if true the next step of the maintenance is loaded, else the previous step
    private void LoadStep(bool nextStep)
    {
        // stop animation from last step
        Debug.Log("stop tool with index:" + _index);
       
        tools[_index].StopMovement();
        int currentIndex = _index;
        if (nextStep)
        {
            _index++;
        }
        else
        {
            _index--;
        }
        if (!LoadToolAndText())
        {
            _index = currentIndex;
            if (_index == _guidelines.guidelines.Count - 1)
            {
                MaintenanceFinished();
            }
        }
        SetVisibilityOfPreviousButton();
    }
    
    /// <summary>
    /// manage visibility of the previous button
    /// </summary>
    private void SetVisibilityOfPreviousButton()
    {
        previousButton.gameObject.SetActive(_index != 0);
    }
    
    /// <summary>
    /// load text and tool animation of current step
    /// </summary>
    /// <returns></returns> true if the tool and text for the current step could be loaded; else false
    private bool LoadToolAndText()
    {
        if (CheckListIndexOutOfRange(_guidelines.guidelines, _index) &&
            CheckListIndexOutOfRange(tools, _index))
        {
            tools[_index].StartMovement();
            LoadText();
            return true;
        }
        return false;
    }
    
    /// <summary>
    /// helper function to check whether an index is in an appropriate range
    /// </summary>
    /// <param name="listToCheck"></param> list where to check the index
    /// <param name="index"></param> value of index to check
    /// <returns></returns> true if index is ok, else false
    private static bool CheckListIndexOutOfRange<T>(List<T> listToCheck, int index)
    {
        if (index < 0 || index > listToCheck.Count - 1)
        {
            return false;
        }
        
        return true;
    }
    
    /// <summary>
    /// change UI when maintenance finished
    /// </summary>
    private void MaintenanceFinished()
    {
        // disable overlay menu (freeze and pause button)
        overlayMenu.SetActive(false);
        // activate panel with text and menu button
        maintenanceFinishedMenu.SetActive(true);
    }
    
}
