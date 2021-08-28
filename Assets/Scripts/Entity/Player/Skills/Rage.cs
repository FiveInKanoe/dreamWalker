using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rage : Skill
{
    [SerializeField] private float activeTime;
    [SerializeField] private float bonusDamage;
    [SerializeField] private float bonusAttackSpeed;
    [SerializeField] private float bonusSpeed;
    [SerializeField] private float growthCoef;
    [SerializeField] private Color rageColor;

    private float endOfEffectTime;
    private Entity entity;

    private Transform sprite;
    private SpriteRenderer spriteRenderer;

    

    new void Start()
    {
        base.Start();
        endOfEffectTime = 0;
        growthCoef = 1.2f;
        entity = GetComponent<Entity>();
        sprite = transform.GetChild(0);
        if (sprite != null)
        {
            spriteRenderer = sprite.GetComponent<SpriteRenderer>();
        }
    }

    public override void Usage()
    {
        entity.Damage += bonusDamage;
        entity.AttackSpeed += bonusAttackSpeed;
        entity.Speed += bonusSpeed;
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

    void FixedUpdate()
    {
        if (Input.GetKey(HotKey) && Time.time > nextUsageTime)
        {
            Usage();
            nextUsageTime = Time.time + CoolDown;
            endOfEffectTime = Time.time + activeTime;
        }
        if (endOfEffectTime != 0 && Time.time > endOfEffectTime)
        {
            ToDefault();
            endOfEffectTime = 0;
        }
    }

    private void ToDefault()
    {
        entity.Damage -= bonusDamage;
        entity.AttackSpeed -= bonusAttackSpeed;
        entity.Speed -= bonusSpeed;
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
