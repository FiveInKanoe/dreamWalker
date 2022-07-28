using System.Collections;
using UnityEngine;

public abstract class ClassControl : ScriptableObject
{
    [SerializeField] private float attackCoolDown;

    public PlayerClass ClassType { get; protected set; }
    protected Player Player { get; private set; }
    protected float AttackCoolDown => attackCoolDown;

    public virtual void Initialize(Player player)
    {
        Player = player;
    }

    public abstract IEnumerator Control();
}
