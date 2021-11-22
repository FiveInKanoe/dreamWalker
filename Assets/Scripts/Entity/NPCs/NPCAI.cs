using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCAI : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private Animator animator;
    private NavMeshAgent agent;

    public GameObject GetPlayer()
    {
        return player;
    }
    public NavMeshAgent GetAgent()
    {
        return agent;
    }
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }
    void Update()
    {
        animator.SetFloat("distance", Vector3.Distance(transform.position, player.transform.position));
        Debug.Log(Vector3.Distance(transform.position, player.transform.position));


    }
}
