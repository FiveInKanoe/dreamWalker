using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*TODO
 - Снести всё к херам
 - Каждый эффект реаоизовать через класс
 
 */

public enum PotionType
{
    HEALTH,
    MANA,
}

[CreateAssetMenu(fileName = "HPPotion", menuName = "Items/Potions/Potion Base")]
public class PotionEffectsBase : ScriptableObject
{
    private List<Effect> listOfEffects;

    private void OnEnable()
    {
        listOfEffects = new List<Effect>
        {
            new HealthEffect(),
            new ManaEffect()
        };
    }

    public void StartEffect(PotionType type, Entity entity)
    {
        listOfEffects.Find(delegate (Effect potEf) { return potEf.Type == type; })?.LaunchEffect(entity);
    }

    private abstract class Effect
    {
        private PotionType type;

        public PotionType Type { get => type; protected set => type = value; }

        public abstract void LaunchEffect(Entity entity);
    }

    private class HealthEffect : Effect
    {
        private float healthRegen;
        public HealthEffect()
        {
            Type = PotionType.HEALTH;
            healthRegen = 150;
        }

        public override void LaunchEffect(Entity entity)
        {
            entity.Hp = entity.Hp + healthRegen > entity.HpMax ? entity.HpMax : entity.Hp + healthRegen;
        }
    }

    private class ManaEffect : Effect
    {
        private float manaRegen;
        public ManaEffect()
        {
            Type = PotionType.MANA;
            manaRegen = 150;
        }

        public override void LaunchEffect(Entity entity)
        {
            entity.Mana = entity.Mana + manaRegen > entity.ManaMax ? entity.ManaMax : entity.Mana + manaRegen;
        }
    }
}






