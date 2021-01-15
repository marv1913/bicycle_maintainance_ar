using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LookAtScript : MonoBehaviour
{

    public Camera m_Camera;
    

    // Update is called once per frame
    void Update()
    {

        transform.LookAt(transform.position + m_Camera.transform.rotation * Vector3.forward,
            m_Camera.transform.rotation * Vector3.up);

        var distance = Vector3.Distance(m_Camera.transform.position, transform.position);
        gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, distance/3);
        gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, distance/3);
        
    }
}
