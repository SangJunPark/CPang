using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[System.Serializable]
public class TransformTweenClip : PlayableAsset, ITimelineClipAsset
{
    public TransformTweenBehaviour template = new TransformTweenBehaviour();
    public ExposedReference<Transform> endLoc;

    public ClipCaps clipCaps
    {
        get { return ClipCaps.Blending; }
    }
	// Factory method that generates a playable based on this asset
	public override Playable CreatePlayable(PlayableGraph graph, GameObject go) {
        var playable = ScriptPlayable<TransformTweenBehaviour>.Create(graph, template);
        TransformTweenBehaviour clone = playable.GetBehaviour();
        clone.endLoc = endLoc.Resolve(graph.GetResolver());
		return playable;
	}
}
