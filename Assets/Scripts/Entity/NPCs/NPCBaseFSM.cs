using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class NPCBaseFSM : StateMachineBehaviour
{
    [SerializeField] private GameObject prefabNPC;
    [SerializeField] private GameObject opponent;
    private NPC npc;

    protected GameObject PrefabNPC { get => prefabNPC; set => prefabNPC = value; }
    protected GameObject Opponent { get => opponent; set => opponent = value; }
    protected NPC NPCinf { get => npc; }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        prefabNPC = animator.gameObject;

        npc = animator.gameObject.GetComponent<NPC>();
        opponent = prefabNPC.GetComponent<NPCAI>().Target;


        npc.View.NPCsAgent.updateRotation = false;
        npc.View.NPCsAgent.updateUpAxis = false;
    }

}
