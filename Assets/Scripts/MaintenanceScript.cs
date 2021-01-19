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
    public GameObject MaintenanceFinishedMenu;

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
        // delete first element, because there introduction text is not a component
        temp.RemoveAt(0);
        _guidelines.guidelines = temp;
        // load first instruction and it's animation
        LoadToolAndText();
        SetVisibilityOfPreviousButton();
    }

    private void LoadText()
    {
        text.text = _guidelines.guidelines[_index].descriptionText;
    }

    public void ShowNextStep()
    {
        LoadStep(true);
    }
    
    public void ShowPreviousStep()
    {
        LoadStep(false);
    }

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

    private void SetVisibilityOfPreviousButton()
    {
        if (_index == 0)
        {
            previousButton.gameObject.SetActive(false);
        }
        else
        {
            previousButton.gameObject.SetActive(true);
        }
    }

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

    private static bool CheckListIndexOutOfRange<T>(List<T> listToCheck, int index)
    {
        if (index < 0 || index > listToCheck.Count - 1)
        {
            return false;
        }
        
        return true;
    }

    private void MaintenanceFinished()
    {
        overlayMenu.SetActive(false);
        MaintenanceFinishedMenu.SetActive(true);
    }
    
}
