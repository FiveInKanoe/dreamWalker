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
        //REWRITE IT
        animator.SetBool("isAttacking", npc.Stats.IsAttacking);
        animator.SetBool("isMoving", npc.Stats.IsMoving);
        // animator.SetInteger("direction", (int)State.WEST);
        //attack (1 поменять на атакующую дистанцию)
        npc.Stats.IsAttacking = NPCFSM.GetFloat("distance") <= 1;

        //следование
        // Debug.Log(NPCFSM.GetFloat("distance"));
        // Debug.Log(animator.GetFloat("distance"));
        // animator.SetInteger("direction", (int)State.EAST);
        if (NPCFSM.GetBehaviour<Patrol>() != null)
        {
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
            }
            if (_angle >= 45 && _angle <= 135)
            {
                Debug.Log("NORTH");
                animator.SetInteger("direction", (int)Direction.NORTH);
            }
            if (_angle > 135 && _angle < 225)
            {
                Debug.Log("WEST");
                // animator.SetInteger("direction", (int)State.WEST);
                animator.SetInteger("direction", (int)Direction.EAST);
            }
            if (_angle >= 225 && _angle <= 315)
            {
                Debug.Log("SOUTH");
                animator.SetInteger("direction", (int)Direction.SOUTH);
            }
        }


        // Debug.Log();
    }
}
