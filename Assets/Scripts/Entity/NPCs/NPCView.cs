using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCView : MonoBehaviour
{
    [SerializeField] private Collider2D npcsCollider;

    [SerializeField] private NavMeshAgent npcsAgent;

    [SerializeField] private Animator spriteAnimator;
    [SerializeField] private Animator stateAnimator;

    public Collider2D NPCsCollider { get => npcsCollider; }
    public NavMeshAgent NPCsAgent { get => npcsAgent; }
    public Animator SpriteAnimator { get => spriteAnimator; }
    public Animator StateAnimator { get => stateAnimator; }
}
