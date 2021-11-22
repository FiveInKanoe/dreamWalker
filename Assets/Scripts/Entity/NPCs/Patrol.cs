using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// using UnityEngine.Scripting.APIUpdating;
public class Patrol : NPCBaseFSM
{
    [SerializeField] Vector2 to;
    public List<Vector3> waypoints { get; set; }
    public int currentWP { get; set; }
    public Vector3 nowGoal { get; set; }
    private bool sleepFlag;
    private float sleepTime;
    private float speed = 1.5f;
    private NavMeshAgent agent;
    public NavMeshPath navMeshPath;
    private float radisWaypoint;
    private int countWaypoint;

    // void Awake()
    // {
    //     // waypoints = GameObject.FindGameObjectsWithTag("waypoint");
    // }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        sleepFlag = true;
        currentWP = 0;
        sleepTime = 0;

        radisWaypoint = 7;
        countWaypoint = 5;

        waypoints = new List<Vector3>();
        waypoints.Add(PrefabNPC.transform.position);
        navMeshPath = new NavMeshPath();
        PrefabNPC.GetComponent<NPCAI>().Agent.acceleration = 60;
        agent = PrefabNPC.GetComponent<NPCAI>().Agent;
        GenerateWayPoints();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Проверка наличия точек
        if (waypoints.Count != 0)
        {

            //Преверка расстояния между точкой НПС
            if (Vector2.Distance(waypoints[currentWP], PrefabNPC.transform.position) < 1.0f)
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
            PrefabNPC.GetComponent<NPCAI>().Agent.SetDestination(waypoints[currentWP]);
            nowGoal = waypoints[currentWP];
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

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
            float _x = Random.Range(PrefabNPC.transform.position.x - radisWaypoint, PrefabNPC.transform.position.x + radisWaypoint);
            float _y = Random.Range(PrefabNPC.transform.position.y - radisWaypoint, PrefabNPC.transform.position.y + radisWaypoint);
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
