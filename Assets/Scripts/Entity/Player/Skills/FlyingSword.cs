using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "FlyingSword", menuName = "Skills/Flying Sword")]
public class FlyingSword : Skills
{
    [SerializeField] private Ammo swordAmmo;
    [SerializeField] private float lifeTime;
    [SerializeField] private float damage;
    [SerializeField] private float swordVelocity;
    [SerializeField] private int maxSwordsCount;

    private GameObject flyingSwordContainer;


    public override void Initialize(Player player, GameObject skillContainer)
    {
        SkillContainer = skillContainer;
        Player = player;
        flyingSwordContainer = new GameObject("FlyingSword");
        flyingSwordContainer.transform.SetParent(SkillContainer.transform);
        swordAmmo.ContainerTransform = flyingSwordContainer.transform;
    }

    public override IEnumerator Usage()
    {
        while (true)
        {
            yield return new WaitUntil(() => Input.GetKey(HotKey));
            Perform();
            yield return new WaitForSecondsRealtime(CoolDown);
        }
    }

    private void Perform()
    {

        if (flyingSwordContainer.transform.childCount < maxSwordsCount)
        {
            swordAmmo.BeShooted(
                Player.transform.position,
                Player.transform.rotation,
                swordVelocity, 
                lifeTime
                );
        }
    }

    
}
