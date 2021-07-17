using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private MovementControl movementControl;
    private IClassControl classControl;

    private Rigidbody2D playersBody;

    [SerializeField] private float damage;
    [SerializeField] private float speed;

    [SerializeField] private float hp;
    [SerializeField] private float mana;

    [SerializeField] private float hpMax;
    [SerializeField] private float manaMax;

    private bool isAlive = true;

    void Start()
    {
        playersBody = this.GetComponent<Rigidbody2D>();
        movementControl = new MovementControl(playersBody, speed);
        classControl = new WarriorControl();

    }

    void Update()
    {
        movementControl.Move();
        classControl.ControlStrategy();
    }
}
