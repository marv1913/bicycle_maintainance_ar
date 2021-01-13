using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ToolMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject toolRotationAxis;
    public GameObject tool;
    public Vector3 rotationVector;
    public Vector3 rotationLimit = new Vector3(-1f, -1f, -1f);
    public bool xShouldBeGreaterThanLimit = true;
    public bool yShouldBeGreaterThanLimit = true;
    public bool zShouldBeGreaterThanLimit = true;
    
    private bool _resetPosition;
    private bool _rotateTool = false;
    private float _angle = 0f;
    
    private Quaternion _defaultRotationTool;
    private Vector3 _defaultRotation;
    void Start()
    {
        _defaultRotationTool = toolRotationAxis.transform.localRotation;
        _defaultRotation = toolRotationAxis.transform.localRotation.eulerAngles;
    }

    public void StartMovement()
    {
        
        if (!_resetPosition)
        {
            Debug.Log("setting active");
            tool.SetActive(true);
            if (tool.activeSelf && !_resetPosition)
            {
                Debug.Log("rotate");
                _rotateTool = true;
            }
        }
    }
    
    public void StopMovement()
    {
        _rotateTool = false;
        tool.SetActive(false);
        Debug.Log("stop moving");
    }
    

    // Update is called once per frame
    void Update()
    {
        if (_rotateTool)
        {
            RotateTool();
        }
    }

    private void RestoreToolPosition()
    {
        bool restoreToDefaultRotation = false;
        if (rotationLimit.x != -1)
        {
            if (xShouldBeGreaterThanLimit && toolRotationAxis.transform.localRotation.eulerAngles.x > rotationLimit.x || !xShouldBeGreaterThanLimit && toolRotationAxis.transform.localRotation.eulerAngles.x < rotationLimit.x)
            {
                Debug.Log("restore");
                restoreToDefaultRotation = true;
            }
        }
        if (rotationLimit.y != -1)
        {
            if (yShouldBeGreaterThanLimit && toolRotationAxis.transform.localRotation.eulerAngles.y > rotationLimit.y || !yShouldBeGreaterThanLimit && toolRotationAxis.transform.localRotation.eulerAngles.y < rotationLimit.y)
            {
                restoreToDefaultRotation = true;
            }
        }
        if (rotationLimit.z != -1)
        {

            if (zShouldBeGreaterThanLimit && toolRotationAxis.transform.localRotation.eulerAngles.z > rotationLimit.z|| !zShouldBeGreaterThanLimit && toolRotationAxis.transform.localRotation.eulerAngles.z < rotationLimit.z)
            {
                restoreToDefaultRotation = true;
            }
        }

        if (restoreToDefaultRotation)
        {
            tool.gameObject.SetActive(false);
            _resetPosition = true;
            Invoke("SetToolActiveAgain", 0.2f);
        }
    }

    private void SetToolActiveAgain()
    {
        toolRotationAxis.transform.localRotation = _defaultRotationTool;
        _defaultRotation = _defaultRotationTool.eulerAngles;
        tool.SetActive(true);
        _resetPosition = false;
    }
    
    private void RotateTool()
    {
        _defaultRotation = _defaultRotation + rotationVector * Time.deltaTime;

     
        toolRotationAxis.transform.localRotation = Quaternion.Euler(_defaultRotation);
        RestoreToolPosition();
    }
}
