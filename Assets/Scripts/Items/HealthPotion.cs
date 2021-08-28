using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : Item
{
    [SerializeField] private float hpRegen;
    [SerializeField] private float coolDown;

    public override void Usage(Player player)
    {
        player.Hp += hpRegen;
    }
}
