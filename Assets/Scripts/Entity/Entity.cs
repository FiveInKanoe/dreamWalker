using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [SerializeField] private float hp;
    [SerializeField] private float mana;
    [SerializeField] private EntityStats stats;

    public float Hp { get => hp; set => hp = value; }
    public float Mana { get => mana; set => mana = value; }
    public EntityStats Stats { get => stats; }

    private void Start()
    {
        stats.IsAlive = true;
        stats.IsMoving = false;
        stats.IsAttacking = false;
    }

}
