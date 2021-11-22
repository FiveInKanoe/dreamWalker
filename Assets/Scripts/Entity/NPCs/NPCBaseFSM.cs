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

    //���������� PrefabNPC � OnEnable ������ � ����������� ������


    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        prefabNPC = animator.gameObject;
        opponent = prefabNPC.GetComponent<NPCAI>().Player;

        prefabNPC.GetComponent<NPCAI>().Agent.updateRotation = false;
        prefabNPC.GetComponent<NPCAI>().Agent.updateUpAxis = false;
    }

}
