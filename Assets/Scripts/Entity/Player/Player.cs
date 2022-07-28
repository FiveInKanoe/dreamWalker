using System.Collections.Generic;
using UnityEngine;


public class Player : Entity
{
    [SerializeField] private PlayerClass playerClass;
    [SerializeField] private List<Skills> skillSet = new List<Skills>();
    [SerializeField] private Inventory inventory;

    [SerializeField] private PlayerComponents components;
    [SerializeField] private List<ClassControl> classControlTypes = new List<ClassControl>();

    private ClassControl classControl;
    private GameObject skillContainer;

    public PlayerClass PlayerClass => playerClass;
    public PlayerComponents Components => components; 
    public Inventory Inventory => inventory; 


    private void Start()
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
        StartCoroutine(classControl.Control());

        foreach (Skills skill in skillSet)
        {
            skill.Initialize(this, skillContainer);
            StartCoroutine(skill.Usage());
        }
    }

    private void Update()
    {
        
    }

    private void LateUpdate()
    {
        components.SpriteRenderer.transform.rotation = Quaternion.identity;
    }

    private void OnApplicationQuit()
    {
        StopAllCoroutines();
        // inventory.ClearInventory();
    }

}
