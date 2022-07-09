
using UnityEngine;

public abstract class NPCState : StateMachineBehaviour
{
    private int distanceId = Animator.StringToHash("distance");
    private int isOnWayPointId = Animator.StringToHash("isOnWayPoint");
    private int isReadyToGoId = Animator.StringToHash("isReadyToGo");

    protected int DistanceId => distanceId;
    protected int IsOnWayPointId => isOnWayPointId;
    protected int IsReadyToGoId => isReadyToGoId;

    protected NPC Npc { get; private set; }


    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Npc == null)
        {
            Npc = animator.gameObject.GetComponent<NPC>();
        }
        
        Npc.Components.NPCsAgent.updateRotation = false;
        Npc.Components.NPCsAgent.updateUpAxis = false;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetFloat(distanceId, Vector3.Distance(Npc.transform.position, Npc.Target.transform.position));
    }

}
