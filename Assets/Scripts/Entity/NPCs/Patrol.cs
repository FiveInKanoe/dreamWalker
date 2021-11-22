using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : NPCBaseFSM
{
    public List<GameObject> Waypoints { get; set; }
    public int CurrentWP { get; set; }
    public Vector3 NowGoal { get; set; }

    void Awake()
    {
        Waypoints = new List<GameObject>();
    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        CurrentWP = 0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Проверка наличия точек
        if (Waypoints.Count == 0)
        {
            return;
        }
        //Преверка расстояния между точкой НПС
        if (Vector3.Distance(Waypoints[CurrentWP].transform.position, PrefabNPC.transform.position) < 1.0f)
        {
            CurrentWP++;
            //Сброс счетчика
            if (CurrentWP >= Waypoints.Count)
            {
                CurrentWP = 0;
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
        PrefabNPC.GetComponent<NPCAI>().GetAgent().SetDestination(Waypoints[CurrentWP].transform.position);
        NowGoal = Waypoints[CurrentWP].transform.position;
        // float angle = Mathf.Atan2(NPC.GetComponent<NPCAI>().GetAgent().transform.position, waypoints[currentWP].transform.position) * Mathf.Rad2Deg;
        // Debug.Log("DESTINATION: " + waypoints[currentWP].transform.position);
        // Debug.Log("angle: " + NPC.);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

}
