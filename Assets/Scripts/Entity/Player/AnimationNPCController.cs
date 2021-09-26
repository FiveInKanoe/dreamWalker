using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationNPCController
{
    enum State
    {
        NORTH = 1, EAST = 2, SOUTH = 3, WEST = 4
    }

    private NPC npc;
    private Animator animator;

    public AnimationNPCController(NPC _npc, Animator animator)
    {
        this.npc = _npc;
        this.animator = animator;
    }

    public void Animate(float angle)
    {
        //REWRITE IT
        animator.SetBool("isAttacking", npc.IsAttacking);
        animator.SetBool("isMoving", npc.IsMoving);

        if (angle > 315 && angle <= 360 || angle >= 0 && angle < 45)
        {
            animator.SetInteger("direction", (int)State.EAST);
        }
        if (angle >= 45 && angle <= 135)
        {
            animator.SetInteger("direction", (int)State.NORTH);
        }
        if (angle > 135 && angle < 225)
        {
            animator.SetInteger("direction", (int)State.WEST);
        }
        if (angle >= 225 && angle <= 315)
        {
            animator.SetInteger("direction", (int)State.SOUTH);
        }
    }
}
