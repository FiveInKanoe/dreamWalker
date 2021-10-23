using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// using UnityEngine.Scripting.APIUpdating;
public class Patrol : NPCBaseFSM
{
    public GameObject[] waypoints { get; set; }
    public int currentWP { get; set; }
    public Vector3 nowGoal { get; set; }
    private bool sleepFlag;
    private float sleepTime;
    private float speed = 1.5f;

    void Awake()
    {
        waypoints = GameObject.FindGameObjectsWithTag("waypoint");
    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        sleepFlag = true;
        currentWP = 0;
        sleepTime = 0;
        base.OnStateEnter(animator, stateInfo, layerIndex);
        NPC.GetComponent<NPCAI>().GetAgent().acceleration = 60;

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("OFFMSAK" + NavMesh.AllAreas);
        // Проверка наличия точек
        if (waypoints.Length == 0)
        {
            return;
        }
        //Преверка расстояния между точкой НПС
        if (Vector3.Distance(waypoints[currentWP].transform.position, NPC.transform.position) < 1.0f)
        {
            if (sleepFlag)
            {
                sleepTime = Time.time + 1;
                sleepFlag = false;
            }
            if (Time.time > sleepTime)
            {
                sleepFlag = true;
                currentWP++;
                //Сброс счетчика
                if (currentWP >= waypoints.Length)
                {
                    currentWP = 0;
                }
            }
        }
        NPC.GetComponent<NPCAI>().GetAgent().SetDestination(waypoints[currentWP].transform.position);
        nowGoal = waypoints[currentWP].transform.position;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

}
