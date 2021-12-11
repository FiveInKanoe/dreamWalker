using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WarriorClass", menuName = "Player Classes/Warrior Class")]
public class WarriorControl : ClassControl
{

    public override void Control()
    {
        //À Ã
        if (Input.GetMouseButton(0))
        {
            Player.Stats.IsAttacking = true;
        }
        else
        {
            Player.Stats.IsAttacking = false;
        }
    }
}
