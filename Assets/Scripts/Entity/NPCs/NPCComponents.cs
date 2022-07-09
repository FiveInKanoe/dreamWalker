using UnityEngine;
using UnityEngine.AI;

public class NPCComponents : MonoBehaviour
{
    [SerializeField] private Rigidbody2D npcsBody;
    
    [SerializeField] private Collider2D npcsCollider;
    [SerializeField] private NavMeshAgent npcsAgent;

    [SerializeField] private Animator spriteAnimator;
    [SerializeField] private Animator stateAnimator;

    public Collider2D NPCsCollider => npcsCollider;
    public Rigidbody2D NPCsBody => npcsBody;
    public NavMeshAgent NPCsAgent => npcsAgent;
    public Animator SpriteAnimator => spriteAnimator;
    public Animator StateAnimator => stateAnimator;
}
