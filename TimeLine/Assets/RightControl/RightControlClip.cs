using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class RightControlClip : PlayableAsset, ITimelineClipAsset
{
    public RightControlBehaviour template = new RightControlBehaviour ();

    public ClipCaps clipCaps
    {
        get { return ClipCaps.Blending; }
    }

    public override Playable CreatePlayable (PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<RightControlBehaviour>.Create (graph, template);
        return playable;
    }
}
