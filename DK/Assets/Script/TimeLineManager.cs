using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TimeLineManager : MonoBehaviour
{
    PlayableDirector director;
    public List<TimelineAsset> timelines;

    static TimeLineManager _instance;

    public static TimeLineManager GetInstance()
    {
        return _instance;
    }

    public void LoadAsset()
    {
        //var loadedAsset = Resources.Load("TimeLines/SceneTL") as TimelineAsset;
        //if(loadedAsset)
        //    timelines.Add(loadedAsset);
        //director.playableAsset = loadedAsset;

        //// iterate through tracks and map the objects appropriately
        //for (var i = 0; i < trackList.Count; i++)
        //{
        //    if (trackList[i] != null)
        //    {
        //        var track = (TrackAsset)timelineAsset.outputs[i].sourceObject;
        //        timeline.SetGenericBinding(track, trackList[i]);
        //    }
        //}
    }

    void Awake()
    {
        _instance = this;
       // director = this.GetComponent<PlayableDirector>();

        //_instance.LoadAsset();
        //_instance.Play();
    }

    public void Play()
    {
        //director.Play();
    }

    public void Preload()
    {

    }
}