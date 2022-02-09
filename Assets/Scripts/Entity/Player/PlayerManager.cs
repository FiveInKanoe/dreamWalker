using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playersBody;
    [SerializeField] private Collider2D playersCollider;

    [SerializeField] private Animator spriteAnimator;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private Collider2D damageAreaCollider;

    public Rigidbody2D PlayersBody { get => playersBody; }
    public Collider2D PlayersCollider { get => playersCollider; }
    public Animator SpriteAnimator { get => spriteAnimator; }
    public SpriteRenderer SpriteRenderer { get => spriteRenderer; }
    public Collider2D DamageAreaCollider { get => damageAreaCollider; }
}
