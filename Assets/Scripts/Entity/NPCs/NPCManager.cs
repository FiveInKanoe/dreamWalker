using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCManager : MonoBehaviour
{
    [SerializeField] private Rigidbody2D npcsBody;
    
    [SerializeField] private Collider2D npcsCollider;
    [SerializeField] private NavMeshAgent npcsAgent;

    [SerializeField] private Animator spriteAnimator;
    [SerializeField] private Animator stateAnimator;

    public Collider2D NPCsCollider { get => npcsCollider; }
    public Rigidbody2D NPCsBody { get => npcsBody; }
    public NavMeshAgent NPCsAgent { get => npcsAgent; }
    public Animator SpriteAnimator { get => spriteAnimator; }
    public Animator StateAnimator { get => stateAnimator; }
}
