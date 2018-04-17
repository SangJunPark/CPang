using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineController : MonoBehaviour {

    public PlayableDirector director;

	void Awake()
    {
        var obj = director.GetGenericBinding(new Object());
    }
}
