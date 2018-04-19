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
        var loaded = Resources.Load("TimeLines/SceneTL") as TimelineAsset;
        if(loaded)
            timelines.Add(loaded);
        director.playableAsset = loaded;
        director.RebuildGraph();
    }

    void Awake()
    {
        _instance = this;
        //director = this.GetComponent<PlayableDirector>();

        //_instance.LoadAsset();
        //_instance.Play();
    }

    public void Play()
    {
        director.Play();
    }

    public void Preload()
    {

    }
}