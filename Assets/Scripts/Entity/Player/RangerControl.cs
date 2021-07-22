using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerControl : IClassControl
{
    public void ControlStrategy(Player player)
    {
        //À Ã
        if (Input.GetMouseButton(0) == true)
        {
            player.IsAttacking = true;
        }
        else
        {
            player.IsAttacking = false;
        }
    }
}
