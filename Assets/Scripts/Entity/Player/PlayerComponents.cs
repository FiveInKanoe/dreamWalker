using UnityEngine;

public class PlayerComponents : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playersBody;
    [SerializeField] private Collider2D playersCollider;

    [SerializeField] private Animator spriteAnimator;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private Collider2D damageAreaCollider;

    public Rigidbody2D PlayersBody => playersBody;
    public Collider2D PlayersCollider => playersCollider;
    public Animator SpriteAnimator => spriteAnimator;
    public SpriteRenderer SpriteRenderer => spriteRenderer;
    public Collider2D DamageAreaCollider => damageAreaCollider;
}
