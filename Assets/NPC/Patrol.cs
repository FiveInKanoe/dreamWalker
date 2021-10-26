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
        waypoints.Add(NPC.transform.position);
        navMeshPath = new NavMeshPath();
        NPC.GetComponent<NPCAI>().GetAgent().acceleration = 60;
        agent = NPC.GetComponent<NPCAI>().GetAgent();
        GenerateWayPoints();

        // GetAgent().CalculatePath(to.transform.position, navMeshPath);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Debug.Log("OFFMSAK" + NavMeshSurface2d.);
        // Проверка наличия точек
        if (waypoints.Count != 0)
        {

            //Преверка расстояния между точкой НПС
            if (Vector2.Distance(waypoints[currentWP], NPC.transform.position) < 1.0f)
            {
                if (sleepFlag)
                {
                    sleepTime = Time.time + 2;
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
            NPC.GetComponent<NPCAI>().GetAgent().SetDestination(waypoints[currentWP]);
            // Debug.Log(NPC.transform.position);
            nowGoal = waypoints[currentWP];
        }
        // agent.CalculatePath(NPC.GetComponent<NPCAI>().GetPlayer().transform.position, navMeshPath);
        // if (navMeshPath.status == NavMeshPathStatus.PathComplete)
        // {
        //     Debug.Log("YES");
        // }
        // else
        // {
        //     Debug.Log("NO");

        // }
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
            float _x = Random.Range(NPC.transform.position.x - radisWaypoint, NPC.transform.position.x + radisWaypoint);
            float _y = Random.Range(NPC.transform.position.y - radisWaypoint, NPC.transform.position.y + radisWaypoint);
            float _z = 0;
            Debug.Log("X: " + _x + " Y: " + _y + " Z: " + _z);
            Vector3 _newWaypoint = new Vector3(_x, _y, _z);
            agent.CalculatePath(_newWaypoint, navMeshPath);
            if (waypoints.Count < countWaypoint)
            {
                if (navMeshPath.status == NavMeshPathStatus.PathComplete)
                {
                    GameObject _temp = new GameObject();
                    _temp.transform.position = _newWaypoint;
                    Debug.Log("YES" + _newWaypoint);
                    waypoints.Add(_newWaypoint);
                }
                else
                {
                    Debug.Log("NO" + _newWaypoint);
                }
            }
        }
        Debug.Log("Count: " + waypoints.Count);
    }
}
