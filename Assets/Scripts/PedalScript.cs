using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PedalScript : MonoBehaviour
{
    public GameObject wrenchRotationAxis;
    public GameObject allenRotationAxis;
    public Dropdown myDropdown;
    public Text scrollbarText;
    public bool leftPedal;
    public Transform canvasPosition;
    public GameObject canvas;
    public float speed = 0.1f;
    public Vector3 canvasPostion;
    public Vector3 canvasRotation;
    private ToolMovement _wrenchMovementScript;
    private ToolMovement _allenMovementScript;
    
    private Quaternion _defaultRotationAllen;
    private Vector3 _lastCanvasPosition;
    
   

    // Start is called before the first frame update
    private void Start()
    {
        _wrenchMovementScript = wrenchRotationAxis.GetComponent<ToolMovement>();
        _allenMovementScript = allenRotationAxis.GetComponent<ToolMovement>();
        
        myDropdown.onValueChanged.AddListener(delegate { MyDropdownValueChangedHandler(myDropdown); });
        SetScrollbarText(myDropdown.value);
        canvas.transform.position = canvasPosition.position;
        _lastCanvasPosition = transform.position;

        // InvokeRepeating("PlaceCanvas", 1f, 0.01f);
    }

    // Update is called once per frame
    void Update()
    {
        if (myDropdown.value == 1)
        {
            _wrenchMovementScript.StartMovement();
            // StartWrenchAnimation();
        }
        else
        {
            _wrenchMovementScript.StopMovement();
        }
        if (myDropdown.value == 2)
        {
            _allenMovementScript.StartMovement();
            // StartWrenchAnimation();
        }
        else
        {
            _allenMovementScript.StopMovement();
        }
        // PlaceCanvas();

    }

    private void RotateGameObject(GameObject gameObj, Vector3 rotationVector)
    {
        Quaternion currentRotation = gameObj.transform.localRotation;

        currentRotation = Quaternion.Euler(currentRotation.eulerAngles + rotationVector);
        gameObj.transform.localRotation = currentRotation;
    }
    

    private void StartAllenAnimation()
    {
        // RotateGameObject(allen, new Vector3(0, 0, 60) * Time.deltaTime);
        // if (allen.transform.rotation.eulerAngles.z > 100)
        // {
        //     allen.transform.localRotation = _defaultRotationAllen;
        // }
    }

    void PlaceCanvas()
    {
        
        Vector3 destination = _lastCanvasPosition - transform.position; 
        _lastCanvasPosition = transform.position;
        
        if (Mathf.Abs(destination.x) > 0.0005f)
        {
            
            canvas.transform.position =
                Vector3.MoveTowards(_lastCanvasPosition, transform.position, speed * Time.deltaTime) + canvasPostion;
            canvas.transform.rotation = transform.rotation * Quaternion.Euler(canvasRotation);
        }
        // Debug.Log(destination.x);
        

    }

    void Destroy()
    {
        myDropdown.onValueChanged.RemoveAllListeners();
    }

    private void MyDropdownValueChangedHandler(Dropdown target)
    {
        SetScrollbarText(target.value);
        Debug.Log(target.itemText.text);
    }

    public void SetDropdownIndex(int index)
    {
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