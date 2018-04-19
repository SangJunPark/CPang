using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class PlayerClip : PlayableAsset, ITimelineClipAsset
{
    public ExposedReference<Transform> destination;
    [HideInInspector]
    public PlayerBehaviour template = new PlayerBehaviour ();

    public ClipCaps clipCaps
    {
        get { return ClipCaps.Blending; }
    }

    public override Playable CreatePlayable (PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<PlayerBehaviour>.Create (graph, template);
        PlayerBehaviour clone = playable.GetBehaviour();
        clone.destination = destination.Resolve(graph.GetResolver());
        return playable;
    }
}
