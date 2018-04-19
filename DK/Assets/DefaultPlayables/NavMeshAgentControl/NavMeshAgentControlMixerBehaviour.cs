using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.AI;

public class NavMeshAgentControlMixerBehaviour : PlayableBehaviour
{
    Vector2 smoothDeltaPosition = Vector2.zero;
    Vector2 velocity = Vector2.zero;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        NavMeshAgent trackBinding = playerData as NavMeshAgent;

        if (!trackBinding)
            return;

        int inputCount = playable.GetInputCount();

        for (int i = 0; i < inputCount; i++)
        {
            float inputWeight = playable.GetInputWeight(i);
            ScriptPlayable<NavMeshAgentControlBehaviour> inputPlayable = (ScriptPlayable<NavMeshAgentControlBehaviour>)playable.GetInput(i);
            NavMeshAgentControlBehaviour input = inputPlayable.GetBehaviour();

            if (inputWeight > 0.5f && !input.destinationSet && input.destination)
            {
                if (!trackBinding.isOnNavMesh)
                    continue;

                if (!input.destinationSet)
                {
                    trackBinding.destination = input.destination.position;
                    trackBinding.updatePosition = false;
                    input.destinationSet = true;
                }
                Transform tr = trackBinding.GetComponent<Transform>();

                Vector3 worldDeltaPosition = trackBinding.nextPosition - tr.position;

                // Map 'worldDeltaPosition' to local space
                float dx = Vector3.Dot(tr.right, worldDeltaPosition);
                float dy = Vector3.Dot(tr.forward, worldDeltaPosition);
                Vector2 deltaPosition = new Vector2(dx, dy);

                // Low-pass filter the deltaMove
                float smooth = Mathf.Min(1.0f, Time.deltaTime / 0.15f);
                smoothDeltaPosition = Vector2.Lerp(smoothDeltaPosition, deltaPosition, smooth);

                // Update velocity if time advances
                if (Time.deltaTime > 1e-5f)
                    velocity = smoothDeltaPosition / Time.deltaTime;

                bool shouldMove = velocity.magnitude > 0.5f && trackBinding.remainingDistance > trackBinding.radius;

                // Update animation parameters
                //anim.SetBool("move", shouldMove);
                //anim.SetFloat("velx", velocity.x);
                //anim.SetFloat("vely", velocity.y);

                //GetComponent<LookAt>().lookAtTargetPosition = trackBinding.steeringTarget + tr.forward;
            }
        }
    }
}
