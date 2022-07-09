using UnityEngine;

public abstract class ClassControl : ScriptableObject
{

    public PlayerClass ClassType { get; protected set; }
    protected Player Player { get; private set; }

    public virtual void Initialize(Player player)
    {
        Player = player;
    }

    public abstract void Control();
}
