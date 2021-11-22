using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class NPCBaseFSM : StateMachineBehaviour
{
    [SerializeField] private GameObject prefabNPC;
    [SerializeField] private GameObject opponent;

    protected GameObject PrefabNPC { get => prefabNPC; set => prefabNPC = value; }
    protected GameObject Opponent { get => opponent; set => opponent = value; }

    //Передавать PrefabNPC в OnEnable только в абстрактном классе


    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        prefabNPC = animator.gameObject;
        opponent = prefabNPC.GetComponent<NPCAI>().GetPlayer();

        prefabNPC.GetComponent<NPCAI>().GetAgent().updateRotation = false;
        prefabNPC.GetComponent<NPCAI>().GetAgent().updateUpAxis = false;
    }
    
}
