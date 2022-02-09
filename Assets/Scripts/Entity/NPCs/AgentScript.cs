using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AgentScript : MonoBehaviour
{
    [SerializeField] private NPC npc;

    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = npc.Manager.NPCsAgent;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(npc.Target.transform.position);
    }
}
