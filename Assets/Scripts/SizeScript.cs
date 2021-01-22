using UnityEngine;

/**
* @author Karl Buklewski, Marvin Rausch
*/

public class SizeScript : MonoBehaviour
{
    public Camera m_Camera;

    private RectTransform rt;

    void Start()
    {
        rt = gameObject.GetComponent<RectTransform>();
    }
    
    // Update is called once per frame
    void LateUpdate()
    {
        // get distance between vuforia ar camera and canvas
        var distance = Vector3.Distance(m_Camera.transform.position, transform.position);
        // change size depending on distance
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, distance / 6);
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, distance / 6);
        
    }
}
