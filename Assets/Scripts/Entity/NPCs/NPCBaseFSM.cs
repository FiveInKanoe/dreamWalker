using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class NPCBaseFSM : StateMachineBehaviour
{
    protected NPC npc;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        npc = animator.gameObject.GetComponent<NPC>();

        //???
        npc.Manager.NPCsAgent.updateRotation = false;
        npc.Manager.NPCsAgent.updateUpAxis = false;
    }

}
