
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : NPCState
{
    [SerializeField] private float wayPointDistance;
    private Vector2 wayPoint;
    private NavMeshAgent agent;

    public Vector2 WayPoint => wayPoint;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        agent = Npc.Components.NPCsAgent;

        agent.acceleration = Npc.Stats.Velocity;

        wayPoint = GenerateWayPoint();
        agent.CalculatePath(wayPoint, new NavMeshPath());
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        if (Vector2.Distance(wayPoint, Npc.transform.position) < 1)
        {
            animator.SetBool(IsOnWayPointId, true);
        }
        agent.SetDestination(WayPoint);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(IsReadyToGoId, false);
    }

    private Vector2 GenerateWayPoint()
    {
        Vector2 npcPosition = Npc.gameObject.transform.position;
        float x = Random.Range(npcPosition.x - wayPointDistance, npcPosition.x + wayPointDistance);
        float y = Random.Range(npcPosition.y - wayPointDistance, npcPosition.y + wayPointDistance);

        return new Vector2(x, y);      
    }

}
