using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "WarriorClass", menuName = "Player Classes/Warrior Class")]
public class WarriorControl : ClassControl
{

    public override IEnumerator Control()
    {
        while (true)
        {
            yield return new WaitUntil(() => Input.GetMouseButton(0));
            Player.IsAttacking = true;
            yield return new WaitForSecondsRealtime(AttackCoolDown);
            Player.IsAttacking = false;
        }
    }
}
