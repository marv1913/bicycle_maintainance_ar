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

    public static bool useWorldSpaceTextfield = true;
    
    public Button previousText;
    public Button nextText;
    public TextMeshProUGUI worldSpaceText;
    
    public Text scrollbarText;
    public ToolScript toolScript;
    public int textLengthPerSite = 100;
    public StablizationScript worldSpaceDescriptionText;
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
    
    public void EnableUI()
    {
        FillDropdownMenu();
    }

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

    private void ShowCurrentText()
    {
        if (useWorldSpaceTextfield)
        {
            _text.text = _descriptionText[_currentDescriptionTextIndex];

        }
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

    void Destroy()
    {
        dropdownMenu.onValueChanged.RemoveAllListeners();
    }

    private void FillDropdownMenu()
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
    
    // private void StopAllAnimations()
    // {
    //     foreach (var tool in tools)
    //     {
    //         ToolMovement toolMovement = tool.GetComponent<ToolMovement>();
    //         toolMovement.StopMovement();
    //     }
    //    
    // }
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

    // private void SaveGuidelines(Guidelines guideline)
    // { 
    //     string json = JsonUtility.ToJson(guideline);
    //     File.WriteAllText(_savefilePath, json);
    // }

    public static Guidelines LoadGuidelines(string savefileName)
    {
        // string json = File.ReadAllText(_savefilePath);
        Debug.Log("Load: " + savefileName);
        TextAsset txt = (TextAsset)Resources.Load(savefileName, typeof(TextAsset));
        string json = txt.text;
        return JsonUtility.FromJson<Guidelines>(json);
    }

    public void LoadAppropriateUIText(string savefileName)
    {
        Debug.Log("loading savefile: " + savefileName);
        _guidelines = LoadGuidelines(savefileName);
        
        _descriptionText = new List<string>();
        dropdownMenu.onValueChanged.AddListener(delegate { MyDropdownValueChangedHandler(dropdownMenu); });
        
        FillDescriptionTextList();
        SetNavigationButtonVisibility();

    }
    
    [System.Serializable]
    public class Guideline
    {
        public string buttonName;
        public string descriptionText;

    }
    
    [System.Serializable]
    public class Guidelines
    {
        public List<Guideline> guidelines=new List<Guideline>();
    }
}
