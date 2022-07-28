using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Rage", menuName = "Skills/Rage")]
public class Rage : Skills
{
    [SerializeField] private float activeTime;
    [SerializeField] private float bonusDamage;
    [SerializeField] private float bonusAttackSpeed;
    [SerializeField] private float bonusVelocity;
    [SerializeField] private float growthCoef;
    [SerializeField] private Color rageColor;



    private Transform spriteTransform;
    private SpriteRenderer spriteRenderer;

    public override void Initialize(Player player, GameObject skillContainer)
    {
        SkillContainer = skillContainer;

        growthCoef = 1.2f;
        Player = player;
        spriteTransform = player.Components.SpriteAnimator.gameObject.transform;

        if (spriteTransform != null)
        {
            spriteRenderer = player.Components.SpriteRenderer;
        }
    }

    public override IEnumerator Usage()
    {
        while (true)
        {
            yield return new WaitUntil( () => Input.GetKey(HotKey) );
            Perform();
            yield return new WaitForSecondsRealtime(activeTime);
            ToDefault();
            yield return new WaitForSecondsRealtime(CoolDown - activeTime);
        }
    }

    private void Perform()
    {
        Player.Stats.Damage += bonusDamage;
        Player.Stats.AttackSpeed += bonusAttackSpeed;
        Player.Stats.Velocity += bonusVelocity;
        if (spriteRenderer != null)
        {
            spriteTransform.localScale = new Vector2
                (
                spriteTransform.localScale.x * growthCoef,
                spriteTransform.localScale.y * growthCoef
                );
            spriteRenderer.color = rageColor;
        }
    }

    private void ToDefault()
    {
        Player.Stats.Damage -= bonusDamage;
        Player.Stats.AttackSpeed -= bonusAttackSpeed;
        Player.Stats.Velocity -= bonusVelocity;
        if (spriteRenderer != null)
        {
            spriteTransform.localScale = new Vector2
                (
                spriteTransform.localScale.x / growthCoef,
                spriteTransform.localScale.y / growthCoef
                );
            spriteRenderer.color = Color.white;
        }
    }
}
