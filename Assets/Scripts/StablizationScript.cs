using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StablizationScript : MonoBehaviour
{
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
        Vector3 currentPosition = referenceObject.transform.position;
            float difference = _lastPosition.x - currentPosition.x;
            difference = Mathf.Abs(difference);
            if (difference > threshold)
            {
                Debug.Log(difference);

                transform.position = currentPosition;
                _lastPosition = currentPosition;
            }
           
     
    }

    public void SetReferenceObject(GameObject referenceObj)
    {
        referenceObject = referenceObj;
    }
}
