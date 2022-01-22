using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCAI : MonoBehaviour
{
    [SerializeField] private NPC npc;

    private GameObject target;

    private Animator animator;
    private NavMeshAgent agent;

    public GameObject Target { get => target; }
    public NavMeshAgent Agent { get => agent; }

    void Start()
    {
        animator = npc.View.StateAnimator;
        agent = npc.View.NPCsAgent;
        target = npc.Target;

        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }
    void Update()
    {
        animator.SetFloat("distance", Vector3.Distance(transform.position, target.transform.position));
        //Debug.Log(Vector3.Distance(transform.position, target.transform.position));
    }
}