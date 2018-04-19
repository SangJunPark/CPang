using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.AI;


public class CFActor : MonoBehaviour {

    public LookAt lookAt;
    public NavMeshAgent agent;
    Vector3 dest;
    float duration;

    bool bMoving;
    private void Awake()
    {
        bMoving = false;
    }
    public void SetDestination(Vector3 destPos, double clipDuration)
    {
        if (!bMoving)
        {
            bMoving = true;
            //agent.updatePosition = false;
            agent.destination = destPos;

            duration = (float)clipDuration;
            //agent.speed = distance / (float)duration;
            //agent.SetDestination(destPos);
            //dest = destPos;
            StartCoroutine(Co_WaitForPath());
        }
    }

    IEnumerator Co_WaitForPath()
    {
        while (agent.pathPending)
            yield return null;

        float distance = agent.remainingDistance;
        agent.speed = distance / duration;
    }
    private void Update()
    {
        if (bMoving)
        {
         //   Debug.Log(agent.remainingDistance);
            //dest = dest * 0.1f;
            //dest = new Vector3(0.1f, 0, 0.1f);
            //agent.Move(dest);
            //Debug.Log("dd");
        }
    }

}
