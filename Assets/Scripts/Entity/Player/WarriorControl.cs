using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorControl : IClassControl
{
    public void ControlStrategy(Player player)
    {
        //À Ã
        if (Input.GetMouseButton(0) == true)
        {
            player.GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
            player.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
}
