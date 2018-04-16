using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

// A behaviour that is attached to a playable
public class TransformTweenMixerBehaviour : PlayableBehaviour
{
	// Called each frame while the state is set to Play
	public override void ProcessFrame(Playable playable, FrameData info, object playerData) {
        Transform trackBinding = playerData as Transform;
        if (trackBinding == null)
            return;

        int inputCount = playable.GetInputCount();
        for(int i = 0; i < inputCount; ++i)
        {
            ScriptPlayable<TransformTweenBehaviour> playableInput = (ScriptPlayable<TransformTweenBehaviour>)playable.GetInput(i);
            TransformTweenBehaviour input = playableInput.GetBehaviour();
            if (input.endLoc == null)
                continue;
            float inputWeight = playable.GetInputWeight(i);
            //blah blah..
        }
	}
}
