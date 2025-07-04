using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Сделать МоноБехом
public class AnimationNPCController : MonoBehaviour
{
    // [SerializeField] Animator
    [SerializeField] private NPC npc;

    private Animator animator;
    private Animator NPCFSM;

    private void Start()
    {
        animator = npc.Manager.SpriteAnimator;
        NPCFSM = npc.Manager.StateAnimator;
    }


    private void FixedUpdate()
    {
        //REWRITE IT
        animator.SetBool("isAttacking", npc.IsAttacking);
        animator.SetBool("isMoving", npc.IsMoving);
        // animator.SetInteger("direction", (int)State.WEST);
        //attack (1 поменять на атакующую дистанцию)
        npc.IsAttacking = NPCFSM.GetFloat("distance") <= 1;

        Vector3 _driectionVecotr3 = npc.Manager.NPCsAgent.velocity;
        // Direction _directionCalculated;
        if (Mathf.Abs(_driectionVecotr3.x) > Mathf.Abs(_driectionVecotr3.y))
        {
            float deltaX = NPCFSM.GetBehaviour<Patrol>().nowGoal.x - npc.transform.position.x;
            float deltaY = NPCFSM.GetBehaviour<Patrol>().nowGoal.y - npc.transform.position.y;

            float _angle = Mathf.Atan2(deltaY, deltaX) * Mathf.Rad2Deg;
            if (_angle < 0)
            {
                _angle = Mathf.Abs(_angle) + 180;
            }
            // float _angle = (Mathf.Abs(Mathf.Atan2(deltaY, deltaX) * Mathf.Rad2Deg));
            //Debug.Log(_angle);
            // if
            //Debug.Log("DIRECTION: " + animator.GetInteger("direction"));
            if (_angle > 315 && _angle <= 360 || _angle >= 0 && _angle < 45)
            {
                //Debug.Log("EAST");
                // animator.SetInteger("direction", (int)State.EAST);
                animator.SetInteger("direction", (int)Direction.WEST);
            }
            else
            {
                //Debug.Log("NORTH");
                animator.SetInteger("direction", (int)Direction.NORTH);
            }
        }
        else
        {
            //Vertical
            if (_driectionVecotr3.y > 0)
            {
                //Debug.Log("WEST");
                // animator.SetInteger("direction", (int)State.WEST);
                animator.SetInteger("direction", (int)Direction.EAST);
            }
            else
            {
                //Debug.Log("SOUTH");
                animator.SetInteger("direction", (int)Direction.SOUTH);
            }
        }
        // animator.SetInteger("direction", (int)_directionCalculated);
    }

}
