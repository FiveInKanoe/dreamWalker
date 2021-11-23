using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : Entity
{

    [SerializeField] private NPCView view;

    [SerializeField] private GameObject target;

    //private int attackRadius = 100;

    public NPCView View { get => view; }
    public Vector3 PosCurrentWP { get; set; }
    public float AttackRadius { get; set; }

    public GameObject Target { get => target; }

    void Start()
    {
        Stats.IsMoving = true;
        Stats.IsAttacking = false;

    }

    void FixedUpdate()
    {
        // Debug.Log("Contains" + navMesh.GetComponent<NavMeshSurface2d>().navMeshData.sourceBounds.Contains(new Vector3(transform.position.x, transform.position.y, navMesh.GetComponent<NavMeshSurface2d>().navMeshData.sourceBounds.extents.z)));
        // Debug.Log("Contains" + navMesh.GetComponent<NavMeshSurface2d>().navMeshData.sourceBounds.Contains(navMesh.GetComponent<NavMeshSurface2d>().navMeshData.sourceBounds.min));
        // Debug.Log("Contains min" + navMesh.GetComponent<NavMeshSurface2d>().navMeshData.sourceBounds.min);
        // Debug.Log("Contains max" + navMesh.GetComponent<NavMeshSurface2d>().navMeshData.sourceBounds.max);
        // Debug.Log("Contains pos" + transform.position);

    }

    private void LateUpdate()
    {
        transform.GetChild(0).rotation = Quaternion.identity;
    }
}
