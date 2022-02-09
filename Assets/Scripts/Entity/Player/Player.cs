using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : Entity
{
    [SerializeField] private PlayerClass playerClass;
    [SerializeField] private List<Skills> skillSet = new List<Skills>();
    [SerializeField] private Inventory inventory;

    [SerializeField] private PlayerManager manager;

    [SerializeField] private List<ClassControl> classControlTypes = new List<ClassControl>();

    private ClassControl classControl;
    private GameObject skillContainer;

    public PlayerClass PlayerClass { get => playerClass; }
    public PlayerManager Manager { get => manager; }
    public Inventory Inventory { get => inventory; }


    void Start()
    {
        skillContainer = new GameObject("Skill Container");
        foreach (ClassControl control in classControlTypes)
        {
            if (control.ClassType == playerClass)
            {
                classControl = control;
                break;
            }    
        }
        classControl.Initialize(this);
        foreach (Skills skill in skillSet)
        {
            skill.Initialize(this, skillContainer);
        }

    }

    private void Update()
    {
        foreach (Skills skill in skillSet)
        {
            skill.Usage();
        }
        classControl.Control();
    }

    void FixedUpdate()
    {
        

    }

    private void LateUpdate()
    {
        manager.SpriteRenderer.transform.rotation = Quaternion.identity;
    }

    void OnApplicationQuit()
    {
        // inventory.ClearInventory();
    }
}
