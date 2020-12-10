
using UnityEngine;
using UnityEngine.UI;

public class PedalScript : MonoBehaviour
{
    public GameObject wrench;
    public GameObject allen;
    public Dropdown myDropdown;
    public Text scrollbarText;
    public bool leftPedal;
    
    private Quaternion _defaultRotationWrench;
    private Quaternion _defaultRotationAllen;

    // Start is called before the first frame update
    private void Start()
    {
        _defaultRotationWrench = wrench.transform.localRotation;
        _defaultRotationAllen = allen.transform.localRotation;

        
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

        if (myDropdown.value == 2)
        {
            allen.SetActive(true);
            StartAllenAnimation();
        }
        else
        {
            allen.SetActive(false);
        }
        
    }

    private void RotateGameObject(GameObject gameObj, Vector3 rotationVector)
    {
        Quaternion currentRotation = gameObj.transform.localRotation;
      
        currentRotation = Quaternion.Euler(currentRotation.eulerAngles + rotationVector);
        gameObj.transform.localRotation = currentRotation;
    }
    
        

    private void StartWrenchAnimation()
    {
        
        Quaternion currentRotation = wrench.transform.localRotation;
        if (leftPedal)
        {
            currentRotation = Quaternion.Euler(currentRotation.eulerAngles + new Vector3(0, 0, -60) * Time.deltaTime);
        
        }
        else
        {
            currentRotation = Quaternion.Euler(currentRotation.eulerAngles + new Vector3(0, 0, 60) * Time.deltaTime);
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

    private void StartAllenAnimation()
    {
        RotateGameObject(allen, new Vector3(0, 0, 60)*Time.deltaTime);
        if (allen.transform.rotation.eulerAngles.z > 100)
        {
            allen.transform.localRotation = _defaultRotationAllen;
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