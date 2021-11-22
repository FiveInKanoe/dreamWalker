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
    private Entity entity;


    private Transform sprite;
    private SpriteRenderer spriteRenderer;

    public override void Init(GameObject gameObject, GameObject skillContainer)
    {
        NextUsageTime = 0;
        this.SkillContainer = skillContainer;
        endOfEffectTime = 0;
        growthCoef = 1.2f;
        entity = gameObject.GetComponent<Entity>();
        sprite = gameObject.transform.GetChild(0);
        if (sprite != null)
        {
            spriteRenderer = sprite.GetComponent<SpriteRenderer>();
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
        entity.Stats.Damage += bonusDamage;
        entity.Stats.AttackSpeed += bonusAttackSpeed;
        entity.Stats.Velocity += bonusVelocity;
        if (spriteRenderer != null)
        {
            sprite.localScale = new Vector2
                (
                sprite.localScale.x * growthCoef,
                sprite.localScale.y * growthCoef
                );
            spriteRenderer.color = rageColor;
        }
    }

    private void ToDefault()
    {
        entity.Stats.Damage -= bonusDamage;
        entity.Stats.AttackSpeed -= bonusAttackSpeed;
        entity.Stats.Velocity -= bonusVelocity;
        if (spriteRenderer != null)
        {
            sprite.localScale = new Vector2
                (
                sprite.localScale.x / growthCoef,
                sprite.localScale.y / growthCoef
                );
            spriteRenderer.color = Color.white;
        }
    }
}
