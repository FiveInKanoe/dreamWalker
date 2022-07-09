using System.Collections.Generic;
using UnityEngine;

public class NPC : Entity
{
    
    [SerializeField] private NPCComponents components;

    [SerializeField] private GameObject target;

    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private List<Item> itemPool = new List<Item>();

    public NPCComponents Components => components;
    public GameObject Target => target;
    public float AttackRadius { get; set; }
    



    void Start()
    {
        IsMoving = true;
        IsAttacking = false;

    }

    void FixedUpdate()
    {

        if(PushDirection != default(Vector2))
        {

            
            
            if ((Vector2)transform.position != PrePushPosition + PushDirection)
            {
                components.NPCsBody.AddForce(PushDirection);
            }
            else
            {


                PushDirection = default(Vector2);
            }

            if (!IsAlive)
            {
                GameObject item = Instantiate(itemPrefab, transform.position, transform.rotation);
                item.GetComponent<Collectable>().ItemInfo = itemPool[Random.Range(0, itemPool.Count)];
                Destroy(gameObject);    
            }
        }
    }

    private void LateUpdate()
    {
        transform.GetChild(0).rotation = Quaternion.identity;
    }

}
