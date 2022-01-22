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
            Player.IsAttacking = true;
        }
        else
        {
            Player.IsAttacking = false;
        }
    }
}
