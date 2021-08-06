using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    [SerializeField] private string playerClass;

    private IClassControl classControl;


    void Start()
    {

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
        classControl.ControlStrategy(this);       
    }

    private void LateUpdate()
    {
        transform.GetChild(0).rotation = Quaternion.identity;
    }
}
