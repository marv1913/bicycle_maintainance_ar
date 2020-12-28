using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{

    public Button previousText;
    public Button nextText;
    public TextMeshProUGUI text;
    public Dropdown dropdownMenu;
    public Text scrollbarText;
    public string savefileName = "testfile.txt";
    public ToolScript toolScript;
    public int textLengthPerSite = 100;
    
    private Guidelines _guidelines;
    private string _savefilePath;
    private GameObject _canvas;
    private List<string> _descriptionText;
    private int _currentDescriptionTextIndex = 0;

    
    
    // Start is called before the first frame update
    void Start()
    {

    }
    
    public void EnableUI()
    {
        this._canvas.SetActive(true);
        FillDropdownMenu();
        
        // scrollbarText.text = _guidelines.guidelines[dropdownMenu.value].descriptionText;
    }

    private void FillDescriptionTextList()
    {
        _descriptionText.Clear();
        string fullText = _guidelines.guidelines[dropdownMenu.value].descriptionText;
        Debug.Log(fullText);
        if (fullText.Length > textLengthPerSite)
        {
            while (fullText.Length > textLengthPerSite)
            {
                string temp = fullText.Substring(0, textLengthPerSite);
                fullText = fullText.Substring(textLengthPerSite);
                _descriptionText.Add(temp);
            }
        }
        _descriptionText.Add(fullText);
        Debug.Log(_descriptionText.Count);
    }

    private void ShowCurrentText()
    {
        text.text = _descriptionText[_currentDescriptionTextIndex];
    }

    public void NextPage()
    {
        if (_currentDescriptionTextIndex < _descriptionText.Count - 1)
        {
            _currentDescriptionTextIndex++;
        }
        SetNavigationButtonVisibility();
    }

    public void PreviousPage()
    {
        if (_currentDescriptionTextIndex != 0)
        {
            _currentDescriptionTextIndex--;
        }
        SetNavigationButtonVisibility();
    }

    private void SetNavigationButtonVisibility()
    {
        if (_currentDescriptionTextIndex == _descriptionText.Count - 1)
        {
            nextText.gameObject.SetActive(false);
        }
        else
        {
            nextText.gameObject.SetActive(true);
        }

        if (_currentDescriptionTextIndex == 0)
        {
            previousText.gameObject.SetActive(false);
        }
        else
        {
            previousText.gameObject.SetActive(true);
        }
        ShowCurrentText();
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
        SetDropdownIndex(1);
        SetDropdownIndex(0);

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
        // scrollbarText.text = _guidelines.guidelines[target.value].descriptionText;
        FillDescriptionTextList();
        SetNavigationButtonVisibility();
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
        
        _descriptionText = new List<string>();
        _canvas = gameObject.transform.GetChild(0).gameObject;
        dropdownMenu.onValueChanged.AddListener(delegate { MyDropdownValueChangedHandler(dropdownMenu); });
        
        FillDescriptionTextList();
        SetNavigationButtonVisibility();

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
