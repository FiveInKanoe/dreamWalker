using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "RangerClass", menuName = "Player Classes/Ranger Class")]
public class RangerControl : ClassControl
{
    [SerializeField] private Ammo ammo;
    [SerializeField] private float ammoLifeTime;
    [SerializeField] private float ammoVelocity;


    private void OnEnable()
    {
        ClassType = PlayerClass.RANGER;
    }

    public override void Initialize(Player player)
    {
        base.Initialize(player);
        ammo.ContainerTransform = new GameObject("Players_Ammo_Container").transform;
    }

    public override IEnumerator Control()
    {
        while (true)
        {
            yield return new WaitUntil(() => Input.GetMouseButton(0));
            PerformAttack();
            Player.IsAttacking = true;
            yield return new WaitForSecondsRealtime(AttackCoolDown);
            Player.IsAttacking = false;
        }
    }

    private void PerformAttack()
    {
        ammo.BeShooted(
            Player.transform.position,
            Player.transform.rotation,
            ammoVelocity,
            ammoLifeTime
            );
    }

    
}
