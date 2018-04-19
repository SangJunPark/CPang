using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class PlayerMixerBehaviour : PlayableBehaviour
{
    CFActor m_TrackBinding;
    bool m_FirstFrameHappened;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        m_TrackBinding = playerData as CFActor;

        if (m_TrackBinding == null)
            return;

        if (!m_FirstFrameHappened)
        {
            m_FirstFrameHappened = true;
        }

        int inputCount = playable.GetInputCount ();

        float totalWeight = 0f;
        float greatestWeight = 0f;
        int currentInputs = 0;

        for (int i = 0; i < inputCount; i++)
        {
            float inputWeight = playable.GetInputWeight(i);
            ScriptPlayable<PlayerBehaviour> inputPlayable = (ScriptPlayable<PlayerBehaviour>)playable.GetInput(i);
            PlayerBehaviour input = inputPlayable.GetBehaviour ();
            
            totalWeight += inputWeight;
            float clipDuration = input.duration;


            if (!Mathf.Approximately (inputWeight, 0f))
                currentInputs++;

            m_TrackBinding.SetDestination(input.destination.position, clipDuration);
            Debug.Log(inputPlayable.GetTime());
        }
    }

    public override void OnPlayableDestroy (Playable playable)
    {
        m_FirstFrameHappened = false;

        if (m_TrackBinding == null)
            return;
    }
}
