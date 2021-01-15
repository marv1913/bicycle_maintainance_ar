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
        gameObject.GetComponentInChildren<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, distance/5);
        gameObject.GetComponentInChildren<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, distance/5);
        
        var textsize = gameObject.GetComponentInChildren<TextMeshPro>().fontScale;

        textsize = textsize + distance;
        //ScaleCanvas();
        
    }

    void ScaleCanvas()
    {
        var scale = gameObject.GetComponent<RectTransform>().localScale;
        var distance = Vector3.Distance(m_Camera.transform.position, transform.position);

        scale = scale * distance*100;

        var textsize = gameObject.GetComponentInChildren<TextMeshPro>().fontSize;
        Debug.Log("Textsize :" + textsize);
        Debug.Log("Distance :" + distance);
        textsize = textsize + distance;
    }
}
