using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MaintenanceScript : MonoBehaviour
{

    public UIScript uiScript;
    public TextMeshProUGUI text;

    private int _index = 0;

    private UIScript.Guidelines _guidelines;
    void Start()
    {
        _guidelines = uiScript.LoadGuidelines(MaintenanceMenu.componentToMaintain);
        LoadText();
    }

    private void LoadText()
    {
        text.text = _guidelines.guidelines[_index].descriptionText;
    }

    public void ShowNextStep()
    {
        Debug.Log("next step");
        if (_index == _guidelines.guidelines.Count-1)
        {
            Debug.Log("Wartung abgeschlossen!");
        }
        else
        {
            _index++;
            LoadText();
        }
    }
    
    
}
