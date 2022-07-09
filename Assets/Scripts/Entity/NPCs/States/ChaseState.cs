using UnityEngine;

public class ChaseState : NPCState
{
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        Npc.Components.NPCsAgent.SetDestination(Npc.Target.transform.position);
    }

}
