using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCAI : MonoBehaviour
{
    [SerializeField] private NPC npc;

    private Animator stateAnimator;
    private NavMeshAgent agent;

    void Start()
    {
        stateAnimator = npc.View.StateAnimator;

        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        stateAnimator.SetFloat("distance", Vector3.Distance(transform.position, npc.Target.transform.position));
    }

}