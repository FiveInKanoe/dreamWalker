using System.Collections;
using System.Collections.Generic;
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


    private float endOfEffectTime;


    private Transform spriteTransform;
    private SpriteRenderer spriteRenderer;

    public override void Initialize(Player player, GameObject skillContainer)
    {
        NextUsageTime = 0;
        SkillContainer = skillContainer;
        endOfEffectTime = 0;
        growthCoef = 1.2f;
        this.Player = player;
        spriteTransform = player.Manager.SpriteAnimator.gameObject.transform;

        if (spriteTransform != null)
        {
            spriteRenderer = player.Manager.SpriteRenderer;
        }
    }

    public override void Usage()
    {
        if (Input.GetKey(HotKey) && Time.time > NextUsageTime)
        {
            Perform();
            NextUsageTime = Time.time + CoolDown;
            endOfEffectTime = Time.time + activeTime;
        }
        if (endOfEffectTime != 0 && Time.time > endOfEffectTime)
        {
            ToDefault();
            endOfEffectTime = 0;
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
