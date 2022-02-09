using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// using UnityEngine.Scripting.APIUpdating;
public class Patrol : NPCBaseFSM
{
    private bool sleepFlag;
    private float sleepTime;
    private NavMeshAgent agent;
    private NavMeshPath navMeshPath;
    private float radisWaypoint;
    private int countWaypoint;

    public List<Vector3> waypoints { get; set; }
    public int currentWP { get; set; }
    public Vector3 nowGoal { get; set; }


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        sleepFlag = true;
        currentWP = 0;
        sleepTime = 0;

        //Можно сделать [SerializedField]
        radisWaypoint = 7;
        countWaypoint = 5;

        waypoints = new List<Vector3>();
        waypoints.Add(npc.gameObject.transform.position);

        navMeshPath = new NavMeshPath();
        npc.Manager.NPCsAgent.acceleration = npc.Stats.Velocity;

        agent = npc.Manager.NPCsAgent;

        GenerateWayPoints();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Проверка наличия точек
        if (waypoints.Count != 0)
        {

            //Преверка расстояния между точкой НПС
            if (Vector2.Distance(waypoints[currentWP], npc.gameObject.transform.position) < 1.0f)
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
                    if (currentWP >= waypoints.Count)
                    {
                        currentWP = 0;
                    }
                }
            }
            npc.Manager.NPCsAgent.SetDestination(waypoints[currentWP]);
            nowGoal = waypoints[currentWP];
        }
    }

    private void GenerateWayPoints()
    {
        int index = 0;
        while (index < 50)
        {
            if (waypoints.Count == countWaypoint)
            {
                break;
            }
            index++;
            Vector2 npcPosition = npc.gameObject.transform.position;
            float _x = Random.Range(npcPosition.x - radisWaypoint, npcPosition.x + radisWaypoint);
            float _y = Random.Range(npcPosition.y - radisWaypoint, npcPosition.y + radisWaypoint);
            float _z = 0;
            Vector3 _newWaypoint = new Vector3(_x, _y, _z);
            agent.CalculatePath(_newWaypoint, navMeshPath);
            if (waypoints.Count < countWaypoint)
            {
                if (navMeshPath.status == NavMeshPathStatus.PathComplete)
                {
                    waypoints.Add(_newWaypoint);
                }
            }
        }
    }
}
