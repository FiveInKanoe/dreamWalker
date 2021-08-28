using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float speed;
    [SerializeField] private float hp;
    [SerializeField] private float mana;
    [SerializeField] private float hpMax;
    [SerializeField] private float manaMax;
    [SerializeField] private bool isAlive;

    public float Damage { get => damage; set => damage = value; }
    public float AttackSpeed { get => attackSpeed; set => attackSpeed = value; }
    public float Speed { get => speed; set => speed = value; }
    public float Hp { get => hp; set => hp = value; }
    public float Mana { get => mana; set => mana = value; }
    public float HpMax { get => hpMax; set => hpMax = value; }
    public float ManaMax { get => manaMax; set => manaMax = value; }
    public bool IsAlive { get => isAlive; set => isAlive = value; }

    public bool IsMoving { get; set; }
    public bool IsAttacking { get; set; }


    void Start()
    {       
        IsAlive = true;
        IsMoving = false;
        IsAttacking = false;
    }

}
