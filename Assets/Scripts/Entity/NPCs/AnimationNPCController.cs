using UnityEngine;


public class AnimationNPCController : MonoBehaviour
{
    [SerializeField] private NPC npc;

    private Animator animator;
    private Animator NPCFSM;

    private void Start()
    {
        animator = npc.Components.SpriteAnimator;
        NPCFSM = npc.Components.StateAnimator;
    }


    private void FixedUpdate()
    {

        animator.SetBool("isAttacking", npc.IsAttacking);
        animator.SetBool("isMoving", npc.IsMoving);

        npc.IsAttacking = NPCFSM.GetFloat("distance") <= 1;

        Vector3 _driectionVecotr3 = npc.Components.NPCsAgent.velocity;

        if (Mathf.Abs(_driectionVecotr3.x) > Mathf.Abs(_driectionVecotr3.y))
        {
            float deltaX = NPCFSM.GetBehaviour<PatrolState>().WayPoint.x - npc.transform.position.x;
            float deltaY = NPCFSM.GetBehaviour<PatrolState>().WayPoint.y - npc.transform.position.y;

            float _angle = Mathf.Atan2(deltaY, deltaX) * Mathf.Rad2Deg;
            if (_angle < 0)
            {
                _angle = Mathf.Abs(_angle) + 180;
            }

            if (_angle > 315 && _angle <= 360 || _angle >= 0 && _angle < 45)
            {
                animator.SetInteger("direction", (int)Direction.WEST);
            }
            else
            {
                animator.SetInteger("direction", (int)Direction.NORTH);
            }
        }
        else
        {
            if (_driectionVecotr3.y > 0)
            {
                animator.SetInteger("direction", (int)Direction.EAST);
            }
            else
            {
                animator.SetInteger("direction", (int)Direction.SOUTH);
            }
        }
    }

}
