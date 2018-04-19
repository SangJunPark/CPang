using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class PlayerBehaviour : PlayableBehaviour
{
    public Transform destination;
    public bool destinationSet;
    public float duration;

    //public override void OnPlayableCreate(Playable playable)
    //{
        
    //}

    public override void OnGraphStart(Playable playable)
    {
        destinationSet = false;
        duration = (float)playable.GetDuration();
        Debug.Log(duration);
    }
}
