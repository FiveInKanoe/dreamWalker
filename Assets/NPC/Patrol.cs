using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : NPCBaseFSM
{
    public GameObject[] waypoints { get; set; }
    public int currentWP { get; set; }
    public Vector3 nowGoal { get; set; }
    private float speed = 1.5f;

    void Awake()
    {
        waypoints = GameObject.FindGameObjectsWithTag("waypoint");
    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        currentWP = 0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Проверка наличия точек
        if (waypoints.Length == 0)
        {
            return;
        }
        //Преверка расстояния между точкой НПС
        if (Vector3.Distance(waypoints[currentWP].transform.position, NPC.transform.position) < 1.0f)
        {
            currentWP++;
            //Сброс счетчика
            if (currentWP >= waypoints.Length)
            {
                currentWP = 0;
            }
        }
        // var direction = waypoints[currentWP].transform.position - NPC.transform.position;
        //Поворот
        // NPC.transform.rotation = Quaternion.Slerp(NPC.transform.rotation, Quaternion.LookRotation(direction), 1.0f * Time.deltaTime);
        // NPC.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(direction.y - NPC.transform.position.y, direction.x - NPC.transform.position.x) * Mathf.Rad2Deg));

        //
        // NPC.transform.Translate(waypoints[currentWP].transform.position.x, waypoints[currentWP].transform.position.y, 0);
        // NPC.transform.position += waypoints[currentWP].transform.position * speed * Time.deltaTime;

        // NPC.transform.position = Vector3.MoveTowards(NPC.transform.position, waypoints[currentWP].transform.position, speed * Time.deltaTime);

        // Debug.Log(this.speed);
        // Debug.Log(Transform.);
        NPC.GetComponent<NPCAI>().GetAgent().SetDestination(waypoints[currentWP].transform.position);
        nowGoal = waypoints[currentWP].transform.position;
        // float angle = Mathf.Atan2(NPC.GetComponent<NPCAI>().GetAgent().transform.position, waypoints[currentWP].transform.position) * Mathf.Rad2Deg;
        // Debug.Log("DESTINATION: " + waypoints[currentWP].transform.position);
        // Debug.Log("angle: " + NPC.);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

}
