using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        Vector3 _driectionVecotr3 = npc.GetComponent<NPCAI>().GetAgent().velocity;
        Direction _directionCalculated;
        if (Mathf.Abs(_driectionVecotr3.x) > Mathf.Abs(_driectionVecotr3.y))
        {
            //Horizontal
            if (_driectionVecotr3.x > 0)
            {
                //right
                _directionCalculated = Direction.EAST;
            }
            else
            {
                //left
                _directionCalculated = Direction.WEST;
            }
        }
        else
        {
            //Vertical
            if (_driectionVecotr3.y > 0)
            {
                //up
                _directionCalculated = Direction.NORTH;
            }
            else
            {
                //down
                _directionCalculated = Direction.SOUTH;
            }
        }
        animator.SetInteger("direction", (int)_directionCalculated);

    }
}
