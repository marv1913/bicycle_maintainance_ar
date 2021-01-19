using UnityEngine;

/**
* @author Karl Buklewski
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
        var distance = Vector3.Distance(m_Camera.transform.position, transform.position);
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, distance / 6);
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, distance / 6);
        
    }
}
