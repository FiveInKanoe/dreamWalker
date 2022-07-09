using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : NPCState
{
    [SerializeField] private float idleTime;
    private float endOfIdleTime;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        endOfIdleTime = Time.time + idleTime;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        if (Time.time > endOfIdleTime)
        {
            animator.SetBool(IsReadyToGoId, true);
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(IsOnWayPointId, false);
    }
}
