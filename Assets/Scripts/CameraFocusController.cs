using UnityEngine;
using Vuforia;

/**
 * this script is necessary, because the vuforia does not set the focus of the smartphone camera automaticall
 * source: https://medium.com/@harmittaa/setting-camera-focus-mode-for-vuforia-arcamera-in-unity-6b3745297c3d
 */
public class CameraFocusController : MonoBehaviour
{
    private bool _mVuforiaStarted = false;

    void Start()
    {
        VuforiaARController vuforia = VuforiaARController.Instance;

        vuforia?.RegisterVuforiaStartedCallback(StartAfterVuforia);
    }

    private void StartAfterVuforia()
    {
        _mVuforiaStarted = true;
        SetAutofocus();
    }

    void OnApplicationPause(bool pause)
    {
        if (!pause)
        {
            // App resumed
            if (_mVuforiaStarted)
            {
                // App resumed and vuforia already started
                // but lets start it again...
                SetAutofocus(); // This is done because some android devices lose the auto focus after resume
                // this was a bug in vuforia 4 and 5. I haven't checked 6, but the code is harmless anyway
            }
        }
    }

    private void SetAutofocus()
    {
        if (CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO))
        {
            Debug.Log("Autofocus set");
        }
        else
        {
            // never actually seen a device that doesn't support this, but just in case
            Debug.Log("this device doesn't support auto focus");
        }
    }
}