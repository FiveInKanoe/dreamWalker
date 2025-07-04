using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skills : ScriptableObject
{
    [SerializeField] private KeyCode hotKey;
    [SerializeField] private float coolDown;
    [SerializeField] private float manaCost;

    protected Player Player { get; set; }

    public KeyCode HotKey { get => hotKey; protected set => hotKey = value; }
    public float CoolDown { get => coolDown; protected set => coolDown = value; }
    public float ManaCost { get => manaCost; protected set => manaCost = value; }

    protected GameObject SkillContainer { get; set; }
    protected float NextUsageTime { get; set; }

    public abstract void Initialize(Player player, GameObject skillContainer);

    public abstract void Usage();
}
