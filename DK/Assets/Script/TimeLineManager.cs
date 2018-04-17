using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TimeLineManager : MonoBehaviour{

    public PlayableDirector director;

    static TimeLineManager _instance;

    public TimeLineManager()
    {
    }

    public static TimeLineManager GetInstance()
    {
        if (_instance == null)
        {
            _instance = new TimeLineManager();
        }
        return _instance;
    }

    public void LoadAsset()
    {

    }
}
