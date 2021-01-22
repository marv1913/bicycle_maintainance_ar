using UnityEngine.Video;
using Vuforia;
/// <summary>
/// could be useful when instructions are extended by videos
/// </summary>
public class videoPlayerScript : DefaultTrackableEventHandler
{
    private VideoPlayer videoPlayer;

    protected override void Start()
    {
        base.Start();
        videoPlayer = transform.GetComponentInChildren<VideoPlayer>();
    }
    
    /// <summary>
    /// play video if target was found
    /// </summary>
    protected override void OnTrackingFound()
    {
        base.OnTrackingFound();
        videoPlayer.Play();
    }
    
    /// <summary>
    /// stop video if target lost
    /// </summary>
    protected override void OnTrackingLost()
    {
        base.OnTrackingLost();
        if (videoPlayer)
            videoPlayer.Stop();
    }

}