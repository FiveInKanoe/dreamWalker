using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorControl : IClassControl
{
    public void ControlStrategy(Player player)
    {
        //���
        if (Input.GetMouseButton(0))
        {
            player.IsAttacking = true;
        }
        else
        {
            player.IsAttacking = false;
        }
    }
}
