
using UnityEngine;
using UnityEngine.UI;

public class PedalScript : MonoBehaviour
{
    public GameObject wrench;
    private Quaternion _defaultRotationWrench;
    public Dropdown myDropdown;
    public Text scrollbarText;
    public bool leftPedal;

    // Start is called before the first frame update
    private void Start()
    {
        _defaultRotationWrench = wrench.transform.localRotation;
        
        myDropdown.onValueChanged.AddListener(delegate {
            MyDropdownValueChangedHandler(myDropdown);
        });
        SetScrollbarText(myDropdown.value);
    }

    // Update is called once per frame
    void Update()
    {
        if (myDropdown.value == 1)
        {
            wrench.SetActive(true);
            StartWrenchAnimation();
        }
        else
        {
            wrench.SetActive(false);
        }
        
    }

    private void StartWrenchAnimation()
    {
        
        Quaternion currentRotation = wrench.transform.localRotation;
        if (leftPedal)
        {
            currentRotation = Quaternion.Euler(currentRotation.eulerAngles + new Vector3(0, 0, -0.5f));
        
        }
        else
        {
            currentRotation = Quaternion.Euler(currentRotation.eulerAngles + new Vector3(0, 0, 0.5f));
        }
        wrench.transform.localRotation = currentRotation;
        if (wrench.transform.localRotation.eulerAngles.z < 110 && leftPedal)
        {
            wrench.transform.localRotation = _defaultRotationWrench;
        }else if (wrench.transform.localRotation.eulerAngles.z > 250 && !leftPedal)
        {
            wrench.transform.localRotation = _defaultRotationWrench;
        }
    }
    
    void Destroy() {
        myDropdown.onValueChanged.RemoveAllListeners();
    }
 
    private void MyDropdownValueChangedHandler(Dropdown target) {
        SetScrollbarText(target.value);
        Debug.Log(target.itemText.text);
    }
 
    public void SetDropdownIndex(int index) {
        myDropdown.value = index;
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
}