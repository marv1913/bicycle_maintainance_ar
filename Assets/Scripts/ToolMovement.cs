using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
/**
* @author Marvin Rausch
* general script for animations of different types of repair tools
*/
public class ToolMovement : MonoBehaviour
{
    public GameObject toolRotationAxis;
    public GameObject tool;
    public Vector3 rotationVector;
    public Vector3 rotationLimit = new Vector3(-1f, -1f, -1f);
    public bool xShouldBeGreaterThanLimit = true;
    public bool yShouldBeGreaterThanLimit = true;
    public bool zShouldBeGreaterThanLimit = true;
    
    private bool _resetPosition;
    private bool _rotateTool = false;
    
    private Quaternion _defaultRotationTool;
    private Vector3 _defaultRotation;
    void Start()
    {
        _defaultRotationTool = toolRotationAxis.transform.localRotation;
        _defaultRotation = toolRotationAxis.transform.localRotation.eulerAngles;
    }
    
    /// <summary>
    /// make tool visibly and start animation
    /// </summary>
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
    
    /// <summary>
    /// make tool invisibly and stop animation
    /// </summary>
    public void StopMovement()
    {
        _rotateTool = false;
        tool.SetActive(false);
        Debug.Log("stop moving");
    }
    

    void Update()
    {
        if (_rotateTool)
        {
            RotateTool();
        }
    }
    
    /// <summary>
    /// reset rotation of tool if current rotation is out of defined range of animation
    /// </summary>
    private void RestoreToolPosition()
    {
        bool restoreToDefaultRotation = false;
        if (rotationLimit.x != -1)
        {
            if (xShouldBeGreaterThanLimit && toolRotationAxis.transform.localRotation.eulerAngles.x > rotationLimit.x || !xShouldBeGreaterThanLimit && toolRotationAxis.transform.localRotation.eulerAngles.x < rotationLimit.x)
            {
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
            Debug.Log(toolRotationAxis.transform.localRotation.eulerAngles.z);
            if (zShouldBeGreaterThanLimit && toolRotationAxis.transform.localRotation.eulerAngles.z > rotationLimit.z|| !zShouldBeGreaterThanLimit && toolRotationAxis.transform.localRotation.eulerAngles.z < rotationLimit.z)
            {
                restoreToDefaultRotation = true;
            }
        }

        if (restoreToDefaultRotation)
        {
            // disable tool for 200ms to get an natural animation
            tool.gameObject.SetActive(false);
            _resetPosition = true;
            Invoke("SetToolActiveAgain", 0.2f);
        }
    }
    
    /// <summary>
    /// reset rotation of tool to default and make it visible again
    /// </summary>
    private void SetToolActiveAgain()
    {
        toolRotationAxis.transform.localRotation = _defaultRotationTool;
        _defaultRotation = _defaultRotationTool.eulerAngles;
        if (_rotateTool)
        {
            tool.SetActive(true);
        }
        _resetPosition = false;
    }
    
    /// <summary>
    /// apply rotation vector on tool to rotate the tool
    /// </summary>
    private void RotateTool()
    {
        _defaultRotation = _defaultRotation + rotationVector * Time.deltaTime;

        toolRotationAxis.transform.localRotation = Quaternion.Euler(_defaultRotation);
        RestoreToolPosition();
    }
}
