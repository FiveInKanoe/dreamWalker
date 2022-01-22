using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : Entity
{
    [SerializeField] private PlayerClass playerClass;
    [SerializeField] private List<Skills> skillSet = new List<Skills>();
    [SerializeField] private Inventory inventory;

    [SerializeField] private PlayerView view;

    [SerializeField] private List<ClassControl> classControlTypes = new List<ClassControl>();

    private ClassControl classControl;
    private GameObject skillContainer;

    public PlayerClass PlayerClass { get => playerClass; }
    public PlayerView View { get => view; }
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

    void FixedUpdate()
    {
        foreach (Skills skill in skillSet)
        {
            skill.Usage();
        }
        classControl.Control();

    }

    private void LateUpdate()
    {
        view.SpriteRenderer.transform.rotation = Quaternion.identity;
    }

    void OnApplicationQuit()
    {
        // inventory.ClearInventory();
    }
}
