using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    [SerializeField] protected GameObject skillContainer;
    [SerializeField] private KeyCode hotKey;
    [SerializeField] private float coolDown;
    [SerializeField] private float manaCost;

    protected float nextUsageTime;

    public KeyCode HotKey { get => hotKey; protected set => hotKey = value; }
    public float CoolDown { get => coolDown; protected set => coolDown = value; }
    public float ManaCost { get => manaCost; protected set => manaCost = value; }

    protected void Start()
    {
        nextUsageTime = 0;
    }

    public abstract void Usage();

    void FixedUpdate()
    {
        if (Input.GetKey(hotKey) && Time.time > nextUsageTime)
        {
            Usage();
            nextUsageTime = Time.time + coolDown;
        }
    }
}
