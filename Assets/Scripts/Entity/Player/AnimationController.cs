using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Entity entity;
    private Animator animator;

    void Start()
    {
        entity = GetComponent<Entity>();
        animator = transform.GetChild(0).gameObject.GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        float angle = transform.rotation.eulerAngles.z;

        animator.SetBool("isAttacking", entity.IsAttacking);
        animator.SetBool("isMoving", entity.IsMoving);

        if (angle > 315 && angle <= 360 || angle >= 0 && angle < 45)
        {
            animator.SetInteger("direction", (int)Direction.EAST);
        }
        if (angle >= 45 && angle <= 135)
        {
            animator.SetInteger("direction", (int)Direction.NORTH);
        }
        if (angle > 135 && angle < 225)
        {
            animator.SetInteger("direction", (int)Direction.WEST);
        }
        if (angle >= 225 && angle <= 315)
        {
            animator.SetInteger("direction", (int)Direction.SOUTH);
        }
    }

}
