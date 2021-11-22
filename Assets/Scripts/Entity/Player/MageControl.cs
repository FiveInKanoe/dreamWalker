using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageControl : IClassControl
{
    public void ControlStrategy(Player player)
    {
        //À Ã
        if (Input.GetMouseButton(0))
        {
            player.Stats.IsAttacking = true;
        }
        else
        {
            player.Stats.IsAttacking = false;
        }
    }
}
