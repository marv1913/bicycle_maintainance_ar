using UnityEngine;

/**
* you can use this script to avoid unwanted movement of child gameobjects because of image noise
* @author Marvin Rausch
*/
public class StabilizationScript : MonoBehaviour
{
    // tolerance for moving 
    public float threshold = 0.03f;
    public GameObject referenceObject;

    private Vector3 _lastPosition;

    // Start is called before the first frame update
    void Start()
    {
        _lastPosition = referenceObject.transform.position;
        transform.position = _lastPosition;
    }

    // Update is called once per frame
    void Update()
    {
        // get distance between current and last position
        Vector3 currentPosition = referenceObject.transform.position;
        float difference = _lastPosition.x - currentPosition.x;
        difference = Mathf.Abs(difference);
        // check whether the distance between the current and last position is larger than the threshold value
        if (difference > threshold)
        {
            transform.position = currentPosition;
            _lastPosition = currentPosition;
        }
    }
    
    /// <summary>
    /// set reference gameobject
    /// should be a child of a vuforia image or object target
    /// should be placed where stabilized gameobject should appear in scene later on
    /// </summary>
    /// <param name="referenceObj"></param>
    public void SetReferenceObject(GameObject referenceObj)
    {
        referenceObject = referenceObj;
    }
}