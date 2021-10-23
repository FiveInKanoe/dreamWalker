using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationNPCController
{
    enum State
    {
        NORTH = 1, EAST = 2, SOUTH = 3, WEST = 4
    }
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
        //REWRITE IT
        animator.SetBool("isAttacking", npc.IsAttacking);
        animator.SetBool("isMoving", npc.IsMoving);
        // animator.SetInteger("direction", (int)State.WEST);
        //attack
        if (NPCFSM.GetFloat("distance") <= 1)
        {
            npc.IsAttacking = true;
        }
        else
        {
            npc.IsAttacking = false;
        }

        //следование
        // Debug.Log(NPCFSM.GetFloat("distance"));
        // Debug.Log(animator.GetFloat("distance"));
        // animator.SetInteger("direction", (int)State.EAST);
        if (NPCFSM.GetBehaviour<Patrol>() != null)
        {
            float deltaX = NPCFSM.GetBehaviour<Patrol>().nowGoal.x - npc.transform.position.x;
            float deltaY = NPCFSM.GetBehaviour<Patrol>().nowGoal.y - npc.transform.position.y;
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
                animator.SetInteger("direction", (int)State.WEST);
            }
            if (_angle >= 45 && _angle <= 135)
            {
                Debug.Log("NORTH");
                animator.SetInteger("direction", (int)State.NORTH);
            }
            if (_angle > 135 && _angle < 225)
            {
                Debug.Log("WEST");
                // animator.SetInteger("direction", (int)State.WEST);
                animator.SetInteger("direction", (int)State.EAST);
            }
            if (_angle >= 225 && _angle <= 315)
            {
                Debug.Log("SOUTH");
                animator.SetInteger("direction", (int)State.SOUTH);
            }
        }


        // Debug.Log();
    }
}
