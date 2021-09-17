using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    [SerializeField] private string playerClass;
    [SerializeField] private List<Skills> skillSet = new List<Skills>();
    [SerializeField] private Inventory inventory;

    private IClassControl classControl;
    private GameObject skillContainer;


    void Start()
    {
        playerClass = playerClass.ToLower();
        skillContainer = new GameObject("Skill Container");
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

        foreach (Skills skill in skillSet)
        {
            skill.Init(gameObject, skillContainer);
        }

    }

    void FixedUpdate()
    {
        foreach (Skills skill in skillSet)
        {
            skill.Usage();
        }
        classControl.ControlStrategy(this);       
    }

    private void LateUpdate()
    {
        transform.GetChild(0).rotation = Quaternion.identity;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Collectable collectableItem = collision.GetComponent<Collectable>();
        if(collectableItem != null && collectableItem.ItemInfo != null)
        {
            inventory.AddItem(collectableItem.ItemInfo, 1);
            collectableItem.ItemInfo.TargetEntity = this;
        }
        Destroy(collectableItem.gameObject);
    }

    void OnApplicationQuit()
    {
        inventory.ClearInventory();
    }
}
