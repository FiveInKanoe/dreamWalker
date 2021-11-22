using UnityEngine;

[CreateAssetMenu(fileName = "EntityStats", menuName = "Stats/Entity Stats")]
public class EntityStats : ScriptableObject
{
    [SerializeField] private float damage;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float velocity;
    [SerializeField] private float hpMax;
    [SerializeField] private float manaMax;

    public float Damage { get => damage; set => damage = value; }
    public float AttackSpeed { get => attackSpeed; set => attackSpeed = value; }
    public float Velocity { get => velocity; set => velocity = value; }
    public float HpMax { get => hpMax; set => hpMax = value; }
    public float ManaMax { get => manaMax; set => manaMax = value; }

    public bool IsAlive { get; set; }
    public bool IsMoving { get; set; }
    public bool IsAttacking { get; set; }

}

