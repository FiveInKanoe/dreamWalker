using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "Inventory")]
public class Inventory : ScriptableObject
{
    /*  TODO:
     *  - Реализовать размер инвентаря 
     *  ("Бесконечный", на данный момент)
     *
     */
    [SerializeField] private int inventorySize;
    [SerializeField] private List<InventorySlot> inventory;

    void OnEnable()
    {
        inventory = new List<InventorySlot>();
    }

    public void AddItem(Item item, int amount)
    {
        bool hasItem = false;     
        for (int i = 0; i < inventory.Count; i++)
        {
            if (item == inventory[i]?.Item)
            {
                inventory[i].Amount += amount;
                hasItem = true;
                break;
            }
        }
        if (!hasItem && item != null)
        {
            inventory.Add(new InventorySlot(item, amount));
        }
    }

    public void ClearInventory()
    {
        inventory.Clear();
    }
}

[System.Serializable]
class InventorySlot
{
    [SerializeField] private Item item;
    [SerializeField] private int amount;

    public Item Item { get => item; set => item = value; }
    public int Amount { get => amount; set => amount = value; }

    public InventorySlot(Item item, int amount)
    {
        this.item = item;
        this.amount = amount;
    }
}
