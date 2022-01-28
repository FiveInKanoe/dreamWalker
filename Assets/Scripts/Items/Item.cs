using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemType
{
    EQUIPMENT,
    POTIONS,
    AMMO
}


public abstract class Item : ScriptableObject
{
    [SerializeField] private int id;
    [SerializeField] private Sprite sprite;
    [SerializeField, TextArea(15, 20)] private string description;

    public int ID { get => id; private set => id = value; }
    public Sprite Sprite{ get => sprite; protected set => sprite = value;}

}
