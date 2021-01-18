using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LookAtScript : MonoBehaviour
{

    public Camera m_Camera;

    private RectTransform rt;

    void Start()
    {
        rt = gameObject.GetComponent<RectTransform>();
    }
    // Update is called once per frame
    void Update()
    {

        transform.LookAt(transform.position + m_Camera.transform.rotation * Vector3.forward,
            m_Camera.transform.rotation * Vector3.up);
        
        var distance = Vector3.Distance(m_Camera.transform.position, transform.position);
        //Resize the Canvas if the distance changes
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, distance / 5);
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, distance / 5);
        
        
    }
}
