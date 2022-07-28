using System.Collections;
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

        float distance = Vector2.Distance(Player.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        ParticleSystem.MainModule main = partSys.main;
        main.startLifetime = distance / 4;

        GameObject l = Instantiate(lightning, Player.transform.position, Player.transform.rotation);
        Destroy(l, 0.7f);
    }
}
