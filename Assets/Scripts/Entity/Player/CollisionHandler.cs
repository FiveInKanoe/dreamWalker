using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private Player player;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (player.PlayerClass == PlayerClass.RANGER)
        {

        }
        if (player.PlayerClass == PlayerClass.MAGE)
        {

        }

        if (player.View.PlayersCollider.IsTouching(other))
        {
            Collectable collectableItem = other.GetComponent<Collectable>();
            if (collectableItem != null)
            {
                if (collectableItem.ItemInfo != null)
                {
                    player.Inventory.AddItem(collectableItem.ItemInfo, 1);
                }
                Destroy(collectableItem.gameObject);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (player.PlayerClass == PlayerClass.WARRIOR)
        {
            if (player.View.DamageAreaCollider.IsTouching(other) && player.IsAttacking)
            {
                NPC enemy = other.GetComponent<NPC>();
                if (enemy != null)
                {
                    Vector2 pushDirection = other.transform.position - transform.position;
                    pushDirection.Normalize();
                    Debug.Log(pushDirection);

                    pushDirection *= 1.5f;

                    enemy.ReciveDamage(player.Stats.Damage, pushDirection);
                }
            }
        }
    }
}
