using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Сделать МоноБехом
public class AnimationNPCController
{
    // [SerializeField] Animator
    private NPC npc;
    private Animator animator;
    private Animator NPCFSM;

    public AnimationNPCController(NPC _npc, Animator animator, Animator _NPCFSM)
    {
        this.npc = _npc;
        this.animator = animator;
        this.NPCFSM = _NPCFSM;
    }

    public void Animate()
    {
<<<<<<< HEAD
        animator.SetBool("isAttacking", npc.IsAttacking);
        animator.SetBool("isMoving", npc.IsMoving);

        //attack
        if (NPCFSM.GetFloat("distance") <= 1)
        {
            npc.IsAttacking = true;
        }
        else
        {
            npc.IsAttacking = false;
        }
=======
        //REWRITE IT
        animator.SetBool("isAttacking", npc.Stats.IsAttacking);
        animator.SetBool("isMoving", npc.Stats.IsMoving);
        // animator.SetInteger("direction", (int)State.WEST);
        //attack (1 поменять на атакующую дистанцию)
        npc.Stats.IsAttacking = NPCFSM.GetFloat("distance") <= 1;
>>>>>>> Vitalii

        Vector3 _driectionVecotr3 = npc.GetComponent<NPCAI>().agent.velocity;
        Direction _directionCalculated;
        if (Mathf.Abs(_driectionVecotr3.x) > Mathf.Abs(_driectionVecotr3.y))
        {
<<<<<<< HEAD
            //Horizontal
            if (_driectionVecotr3.x > 0)
            {
                //right
                _directionCalculated = Direction.EAST;
=======
            float deltaX = NPCFSM.GetBehaviour<Patrol>().NowGoal.x - npc.transform.position.x;
            float deltaY = NPCFSM.GetBehaviour<Patrol>().NowGoal.y - npc.transform.position.y;
            float _angle = Mathf.Atan2(deltaY, deltaX) * Mathf.Rad2Deg;
            if (_angle < 0)
            {
                _angle = Mathf.Abs(_angle) + 180;
            }
            // float _angle = (Mathf.Abs(Mathf.Atan2(deltaY, deltaX) * Mathf.Rad2Deg));
            Debug.Log(_angle);
            // if
            Debug.Log("DIRECTION: " + animator.GetInteger("direction"));
            if (_angle > 315 && _angle <= 360 || _angle >= 0 && _angle < 45)
            {
                Debug.Log("EAST");
                // animator.SetInteger("direction", (int)State.EAST);
                animator.SetInteger("direction", (int)Direction.WEST);
>>>>>>> Vitalii
            }
            else
            {
<<<<<<< HEAD
                //left
                _directionCalculated = Direction.WEST;
=======
                Debug.Log("NORTH");
                animator.SetInteger("direction", (int)Direction.NORTH);
>>>>>>> Vitalii
            }
        }
        else
        {
            //Vertical
            if (_driectionVecotr3.y > 0)
            {
<<<<<<< HEAD
                //up
                _directionCalculated = Direction.NORTH;
=======
                Debug.Log("WEST");
                // animator.SetInteger("direction", (int)State.WEST);
                animator.SetInteger("direction", (int)Direction.EAST);
>>>>>>> Vitalii
            }
            else
            {
<<<<<<< HEAD
                //down
                _directionCalculated = Direction.SOUTH;
=======
                Debug.Log("SOUTH");
                animator.SetInteger("direction", (int)Direction.SOUTH);
>>>>>>> Vitalii
            }
        }
        animator.SetInteger("direction", (int)_directionCalculated);

    }
}
