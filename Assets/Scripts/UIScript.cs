using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Vuforia;
using Button = UnityEngine.UI.Button;

/**
* @author Marvin Rausch
*/

public class UIScript : MonoBehaviour
{

    public static bool useWorldSpaceTextfield = false;
    
    public Button previousText;
    public Button nextText;
    public TextMeshProUGUI worldSpaceText;
    
    public Text scrollbarText;
    public ToolScript toolScript;
    public int textLengthPerSite = 100;
    public StabilizationScript worldSpaceDescriptionText;
    public GameObject worldSpaceCanvas;
    public GameObject textScrollView;
    public TMP_Dropdown dropdownMenu;

    private Guidelines _guidelines;
    private List<string> _descriptionText;
    private int _currentDescriptionTextIndex = 0;
    private bool _cameraIsFreezed = false;
    private TextMeshProUGUI _text;
    
    
    // Start is called before the first frame update
    void Start()
    {
        if (null != textScrollView && null != worldSpaceCanvas)
        {
            if (useWorldSpaceTextfield)
            {
                _text = worldSpaceText;
                textScrollView.SetActive(false);
                worldSpaceCanvas.SetActive(true);
            }
            else
            {
                _text = textScrollView.GetComponentInChildren<TextMeshProUGUI>();
                worldSpaceCanvas.SetActive(false);
                textScrollView.SetActive(true);

            }
        }
    }
    
    /// <summary>
    /// create string list from string to distribute text over multiple pages
    /// </summary>
    private void FillDescriptionTextList()
    {
        if (useWorldSpaceTextfield)
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
        }
    }
    
    /// <summary>
    /// show text of current page
    /// </summary>
    private void ShowCurrentText()
    {
        if (useWorldSpaceTextfield)
        {
            _text.text = _descriptionText[_currentDescriptionTextIndex];

        }
    }
    
    /// <summary>
    /// go to next page
    /// </summary>
    public void NextPage()
    {
        if (_currentDescriptionTextIndex < _descriptionText.Count - 1)
        {
            _currentDescriptionTextIndex++;
        }
        SetNavigationButtonVisibility();
    }
    
    /// <summary>
    /// go to previous page
    /// </summary>
    public void PreviousPage()
    {
        if (_currentDescriptionTextIndex != 0)
        {
            _currentDescriptionTextIndex--;
        }
        SetNavigationButtonVisibility();
    }
    
    /// <summary>
    /// set visibility of text navigation button in world space canvas mode
    /// </summary>
    private void SetNavigationButtonVisibility()
    {
        if (useWorldSpaceTextfield)
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
        else
        {
            previousText.gameObject.SetActive(false);
            nextText.gameObject.SetActive(false);
        }
        
    }

    /// <summary>
    /// load all available options and fill dropdown menu
    /// </summary>
    public void FillDropdownMenu()
    {
        dropdownMenu.options.Clear();
        foreach (var guideline in _guidelines.guidelines)
        {
            dropdownMenu.options.Add(new TMP_Dropdown.OptionData(guideline.buttonName));
        }

        if (dropdownMenu.options.Count > 1)
        {
            SetDropdownIndex(1);
            SetDropdownIndex(0);
        }
      
        Debug.Log("dropdown filled");
    }
    
    /// <summary>
    /// stop camera
    /// you can still interact with the UI
    /// </summary>
    public void FreezeCamera()
    {
        if (_cameraIsFreezed)
        {
            VuforiaRenderer.Instance.Pause(false);
            _cameraIsFreezed = false;
        }
        else
        {
            VuforiaRenderer.Instance.Pause(true);
            _cameraIsFreezed = true;
        }
    }
    
    /// <summary>
    /// is called if user has interacted with the dropdown menu
    /// </summary>
    /// <param name="target"></param> target dropdown menu
    private void MyDropdownValueChangedHandler(TMP_Dropdown target)
    {
        toolScript.StartAnimation(target.value);
        
        if (useWorldSpaceTextfield)
        {
            FillDescriptionTextList();
            SetNavigationButtonVisibility();
        }
        else
        {
            _text.text = _guidelines.guidelines[target.value].descriptionText;
        }
    }
    
    /// <summary>
    /// set current option of dropdown menu
    /// </summary>
    /// <param name="index"></param> index of option you want to set
    public void SetDropdownIndex(int index)
    {
        dropdownMenu.value = index;
    }

    /// <summary>
    /// helper function to generate JSON guideline file from script
    /// </summary>
    /// <param name="guideline"></param> guideline object which should be exported as JSON
    /// <param name="savefilePath"></param> directory where JSON file should be saved
    private void SaveGuidelines(Guidelines guideline, string savefilePath)
    { 
        string json = JsonUtility.ToJson(guideline);
        File.WriteAllText(savefilePath, json);
    }
    
    /// <summary>
    /// load Guidelines from JSON file
    /// </summary>
    /// <param name="savefileName"></param> directory of JSON file
    /// <returns></returns> Guidelines obj loaded from JSON file
    public static Guidelines LoadGuidelines(string savefileName)
    {
        // string json = File.ReadAllText(_savefilePath);
        Debug.Log("Load: " + savefileName);
        TextAsset txt = (TextAsset)Resources.Load(savefileName, typeof(TextAsset));
        string json = txt.text;
        return JsonUtility.FromJson<Guidelines>(json);
    }
    
    /// <summary>
    /// load guidelines from JSON file and fill UI with information
    /// </summary>
    /// <param name="savefileName"></param>
    public void LoadAppropriateUIText(string savefileName)
    {
        Debug.Log("loading savefile: " + savefileName);
        _guidelines = LoadGuidelines(savefileName);
        
        _descriptionText = new List<string>();
        dropdownMenu.onValueChanged.AddListener(delegate { MyDropdownValueChangedHandler(dropdownMenu); });
        
        FillDescriptionTextList();
        SetNavigationButtonVisibility();
    }
    
    /// <summary>
    /// class for guideline object
    /// has properties for button name and description text
    /// </summary>
    [System.Serializable]
    public class Guideline
    {
        public string buttonName;
        public string descriptionText;
    }
    
    /// <summary>
    /// Guidelines are saved as JSON and loaded from JSON file
    /// contains a List of Guideline objects
    /// </summary>
    [System.Serializable]
    public class Guidelines
    {
        public List<Guideline> guidelines=new List<Guideline>();
    }
}
