using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MageClass", menuName = "Player Classes/Mage Class")]
public class MageControl : ClassControl
{
    private void OnEnable()
    {
        ClassType = PlayerClass.MAGE;
    }

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
