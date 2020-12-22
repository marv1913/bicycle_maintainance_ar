using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{

    public Button previousText;
    public Button nextText;
    public Text text;
    public Dropdown dropdownMenu;
    public Text scrollbarText;
    public string savefileName = "testfile.txt";
    public ToolScript toolScript;
    
    private Guidelines _guidelines;
    private string _savefilePath;
    private GameObject _canvas;

    
    
    // Start is called before the first frame update
    void Start()
    {
       
        _canvas = gameObject.transform.GetChild(0).gameObject;
        dropdownMenu.onValueChanged.AddListener(delegate { MyDropdownValueChangedHandler(dropdownMenu); });


    }
    
    public void EnableUI()
    {
        this._canvas.SetActive(true);
        FillDropdownMenu();
        
        scrollbarText.text = _guidelines.guidelines[dropdownMenu.value].descriptionText;
    }
    
    public void DisableUI()
    {
        this._canvas.SetActive(false);
    }
    
    void Destroy()
    {
        dropdownMenu.onValueChanged.RemoveAllListeners();
    }

    private void FillDropdownMenu()
    {
        dropdownMenu.options.Clear();
        foreach (var guideline in _guidelines.guidelines)
        {
            dropdownMenu.options.Add(new Dropdown.OptionData(guideline.buttonName));
        }
    }

    // private void StopAllAnimations()
    // {
    //     foreach (var tool in tools)
    //     {
    //         ToolMovement toolMovement = tool.GetComponent<ToolMovement>();
    //         toolMovement.StopMovement();
    //     }
    //    
    // }
    private void MyDropdownValueChangedHandler(Dropdown target)
    {
        toolScript.StartAnimation(target.value);
        scrollbarText.text = _guidelines.guidelines[target.value].descriptionText;
    }

    public void SetDropdownIndex(int index)
    {
        dropdownMenu.value = index;
    }

    private void SetScrollbarText(int index)
    {
        switch (index)
        {
            case 0:
                scrollbarText.text = "this is the right pedal";
                break;
            case 1:
                scrollbarText.text = "use a wrench moving to shown direction to remove the pedal";
                break;
        }
    }

    private void SaveGuidelines(Guidelines guideline)
    { 
        string json = JsonUtility.ToJson(guideline);
        File.WriteAllText(_savefilePath, json);
    }

    private Guidelines LoadGuidelines()
    {
        string json = File.ReadAllText(_savefilePath);
        return JsonUtility.FromJson<Guidelines>(json);
    }

    public void LoadAppropriateUIText(string savefileName)
    {
        _savefilePath = Application.dataPath + "/" + savefileName;
        _guidelines = LoadGuidelines();

    }
    
    [System.Serializable]
    private class Guideline
    {
        public string buttonName;
        public string descriptionText;

    }
    
    [System.Serializable]
    private class Guidelines
    {
        public List<Guideline> guidelines=new List<Guideline>();
    }
}
