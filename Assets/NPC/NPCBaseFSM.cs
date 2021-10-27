using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCBaseFSM : StateMachineBehaviour
{
    public GameObject NPC;
    public GameObject opponent;
    public float speed = 3.0f;
    public float accuracy = 1.0f;
    private NavMeshAgent agent;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        NPC = animator.gameObject;
        opponent = NPC.GetComponent<NPCAI>().GetPlayer();
        agent = NPC.GetComponent<NPCAI>().agent;

        NPC.GetComponent<NPCAI>().agent.updateRotation = false;
        NPC.GetComponent<NPCAI>().agent.updateUpAxis = false;
    }
}
