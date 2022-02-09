using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MageClass", menuName = "Player Classes/Mage Class")]
public class MageControl : ClassControl
{
    [SerializeField] private GameObject lightning;
    private ParticleSystem partSys;

    public void OnEnable()
    {
        ClassType = PlayerClass.MAGE;
        partSys = lightning.GetComponent<ParticleSystem>();
    }

    public override void Initialize(Player player)
    {
        base.Initialize(player);
        partSys = lightning.GetComponent<ParticleSystem>();
    }

    public override void Control()
    {
        //À Ã
        if (Input.GetMouseButtonDown(0))
        {
            PerformAttack();
            Player.IsAttacking = true;
        }
        else
        {
            Player.IsAttacking = false;
        }
    }

    private void PerformAttack()
    {

        float distance = Vector2.Distance(Player.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        ParticleSystem.MainModule main = partSys.main;
        main.startLifetime = distance / 4;

        GameObject l = Instantiate(lightning, Player.transform.position, Player.transform.rotation);
        Destroy(l, 0.7f);
    }
}
