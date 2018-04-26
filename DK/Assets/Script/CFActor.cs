using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.AI;


public class CFActor : MonoBehaviour {

    Animator animator;
    float h;
    float v;

    public LookAt lookAt;
    public NavMeshAgent agent;
    Vector3 dest;
    float duration;

    bool bMoving;
    private void Awake()
    {
        bMoving = false;
        animator = GetComponent<Animator>();
    }
    public void SetDestination(Vector3 destPos, double clipDuration)
    {
        if (!bMoving)
        {
            bMoving = true;
            //agent.updatePosition = ;
            agent.destination = destPos;

            duration = (float)clipDuration;
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.Play("Lie", -1, 0);
        }
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        if (v > 0)
            Debug.Log(v);

        animator.SetFloat("h", h);
        animator.SetFloat("v", v);
    }

}
