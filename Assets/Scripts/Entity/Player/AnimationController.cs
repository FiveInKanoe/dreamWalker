using System;
using System.Collections.Generic;
using UnityEngine;

enum State
{
    IDLE, MELEE, RUN
}

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Player player;

    private Animator animator;
    private Direction currentDirection;

    private Dictionary<State, Dictionary<Direction, int>> stateSet;


    void Start()
    {
        animator = player.Components.SpriteAnimator;

        State[] states = (State[])Enum.GetValues(typeof(State));
        Direction[] directions = (Direction[])Enum.GetValues(typeof(Direction));

        stateSet = new Dictionary<State, Dictionary<Direction, int>>();
        for (int i = 0; i < states.Length; i++)
        {
            Dictionary<Direction, int> stateVariants = new Dictionary<Direction, int>();
            for (int j = 0; j < directions.Length; j++)
            {
                stateVariants.Add(directions[j], Animator.StringToHash($"{states[i].ToString().ToLower()}_{directions[j].ToString().ToLower()}"));
                Debug.Log($"{states[i].ToString().ToLower()}_{directions[j].ToString().ToLower()}");
            }
            stateSet.Add(states[i], stateVariants);
        }
    }

    void FixedUpdate()
    {
        float angle = transform.rotation.eulerAngles.z;

        if (angle > 315 && angle <= 360 || angle >= 0 && angle < 45)
        {
            currentDirection = Direction.EAST;
        }
        if (angle >= 45 && angle <= 135)
        {
            currentDirection = Direction.NORTH;
        }
        if (angle > 135 && angle < 225)
        {
            currentDirection = Direction.WEST;
        }
        if (angle >= 225 && angle <= 315)
        {
            currentDirection = Direction.SOUTH;
        }

        if (player.IsAttacking)
        {
            animator.Play(stateSet[State.MELEE][currentDirection]);
        }
        else if (player.IsMoving)
        {
            animator.Play(stateSet[State.RUN][currentDirection]);
        }
        else
        {
            animator.Play(stateSet[State.IDLE][currentDirection]);
        }

    }

}
