using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerControl : IClassControl
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
