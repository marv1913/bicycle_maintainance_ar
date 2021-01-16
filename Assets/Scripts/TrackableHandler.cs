using UnityEngine;
using UnityEngine.Serialization;
/**
* @author Marvin Rausch
*/
public class TrackableHandler : MonoBehaviour
{
    public GameObject ui;
    private DefaultTrackableEventHandler handler;

    private void Start()
    {
        handler = GetComponent<DefaultTrackableEventHandler>();
        handler.OnTargetFound.AddListener(OnTrackingFound);
        handler.OnTargetLost.AddListener(OnTrackingLost);
    }

    private void OnTrackingFound()
    {
        ui.SetActive(true);
    }

    private void OnTrackingLost()
    {
        if (null != ui)
        {
            ui.SetActive(false);
        }
    }
}