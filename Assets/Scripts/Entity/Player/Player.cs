using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    [SerializeField] private string playerClass;

    private float viewAngle;

    public bool IsMoving { get; set; }
    public bool IsAttacking { get; set; }

    private MovementControl movementControl;
    private IClassControl classControl;
    private AnimationController animController;

    private GameObject spriteObject;

    void Start()
    {
        IsMoving = false;
        IsAttacking = false;

        spriteObject = transform.GetChild(0).gameObject;

        movementControl = new MovementControl(this);
        animController = new AnimationController(this, spriteObject.GetComponent<Animator>());

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

    void FixedUpdate()
    {
        
        viewAngle = transform.rotation.eulerAngles.z;
        movementControl.Move();
        classControl.ControlStrategy(this);
        animController.Animate(viewAngle);
        
    }

    private void LateUpdate()
    {
        transform.GetChild(0).rotation = Quaternion.identity;
    }
}
