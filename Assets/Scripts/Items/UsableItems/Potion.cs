using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HPPotion", menuName = "Items/Potions/Potion")]
public class Potion : UsableItem
{

    [SerializeField] private PotionType potionType;
    [SerializeField] private PotionEffectsBase potionBase;

    public override void Usage()
    {
        potionBase?.StartEffect(potionType, TargetEntity);
    }
    
    
}
