using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : Entity
{
    
    [SerializeField] private NPCManager manager;

    [SerializeField] private GameObject target;

    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private List<Item> itemPool = new List<Item>();

    //private int attackRadius = 100;

    public NPCManager Manager { get => manager; }

    public float AttackRadius { get; set; }

    public GameObject Target { get => target; }



    void Start()
    {
        IsMoving = true;
        IsAttacking = false;

    }

    void FixedUpdate()
    {
        // Debug.Log("Contains" + navMesh.GetComponent<NavMeshSurface2d>().navMeshData.sourceBounds.Contains(new Vector3(transform.position.x, transform.position.y, navMesh.GetComponent<NavMeshSurface2d>().navMeshData.sourceBounds.extents.z)));
        // Debug.Log("Contains" + navMesh.GetComponent<NavMeshSurface2d>().navMeshData.sourceBounds.Contains(navMesh.GetComponent<NavMeshSurface2d>().navMeshData.sourceBounds.min));
        // Debug.Log("Contains min" + navMesh.GetComponent<NavMeshSurface2d>().navMeshData.sourceBounds.min);
        // Debug.Log("Contains max" + navMesh.GetComponent<NavMeshSurface2d>().navMeshData.sourceBounds.max);
        // Debug.Log("Contains pos" + transform.position);
        //Debug.Log("Pre push position: " + PrePushPosition);
        //Debug.Log("Push direction: " + PushDirection);
        if(PushDirection != default(Vector2))
        {
            //!!!
            //view.NPCsAgent.enabled = false;
            //view.StateAnimator.enabled = false;
            
            
            if ((Vector2)transform.position != PrePushPosition + PushDirection)
            {
                manager.NPCsBody.AddForce(PushDirection);
            }
            else
            {
                //!!!
                //view.StateAnimator.enabled = true;
                //view.NPCsAgent.enabled = true;

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
