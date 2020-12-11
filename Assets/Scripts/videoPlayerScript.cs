using UnityEngine.Video;
using Vuforia;
public class videoPlayerScript : DefaultTrackableEventHandler
{
    private VideoPlayer videoPlayer;

    protected override void Start()
    {
        base.Start();

        videoPlayer = transform.GetComponentInChildren<VideoPlayer>();
    }

    protected override void OnTrackingFound()
    {
        base.OnTrackingFound();
        videoPlayer.Play();
    }

    protected override void OnTrackingLost()
    {
        base.OnTrackingLost();
        if (videoPlayer)
            videoPlayer.Stop();
    }

}