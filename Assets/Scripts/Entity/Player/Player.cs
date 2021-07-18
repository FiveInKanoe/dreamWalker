using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    [SerializeField] private string playerClass;

    private MovementControl movementControl;
    private IClassControl classControl;

    private Rigidbody2D playersBody;

    void Start()
    {
        playersBody = GetComponent<Rigidbody2D>();
        movementControl = new MovementControl();

        playerClass = playerClass.ToLower();

        switch (playerClass)
        {
            case "ranger":
                classControl = new RangerControl();
                break;
            case "mage":
                classControl = new MageControl();
                break;
            default:
                classControl = new WarriorControl();
                break;
        }     
    }

    void Update()
    {
        movementControl.Move(playersBody, GetComponent<Collider2D>() , Speed);
        classControl.ControlStrategy(this);
    }
}
