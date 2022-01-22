using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [SerializeField] private float hp;
    [SerializeField] private float mana;
    [SerializeField] private EntityStats stats;
    [SerializeField] private bool isAlive;
    
    protected Vector2 PushDirection { get; set; }
    protected Vector2 PrePushPosition { get; private set; }

    public float Hp { get => hp; set => hp = value; }
    public float Mana { get => mana; set => mana = value; }
    public EntityStats Stats { get => stats; }

    public bool IsAlive { get => isAlive; set => isAlive = value; }
    public bool IsMoving { get; set; }
    public bool IsAttacking { get; set; }

    public void ReciveDamage(float damage, Vector2 pushDirection = default(Vector2))
    {
        Debug.Log("Damage recived!");
        hp -= damage;
        if (hp <= 0)
        {
            IsAlive = false;
        }
        if (pushDirection != default(Vector2))
        {
            PrePushPosition = transform.position;
        }
        PushDirection = pushDirection;
    }

}
