using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeScript : MonoBehaviour
{
    public Camera m_Camera;

    // Update is called once per frame
    void Update()
    {
        var distance = Vector3.Distance(m_Camera.transform.position, transform.position);
        //gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, distance / 5);
        //gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, distance / 5);

        RectTransform rt= gameObject.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(rt.sizeDelta.x * distance/10, rt.sizeDelta.y * distance/10);
    }
}
