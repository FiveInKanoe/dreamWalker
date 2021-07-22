using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController
{
    enum State
    {
        NORTH = 1, EAST = 2, SOUTH = 3, WEST = 4
    }

    private Player player;
    private Animator animator;

    public AnimationController(Player player, Animator animator)
    {
        this.player = player;
        this.animator = animator;
    }

    public void Animate(float angle)
    {
        animator.SetBool("isAttacking", player.IsAttacking);
        animator.SetBool("isMoving", player.IsMoving);

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
