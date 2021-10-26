using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : Entity
{
    [SerializeField] private static GameObject navMesh;
    private float viewAngle;
    public Vector3 posCurrentWP;
    public bool IsMoving { get; set; }
    public bool IsAttacking { get; set; }
    public int attackRadius = 100;

    private AnimationNPCController animController;

    private GameObject spriteObject;
    void Start()
    {
        IsMoving = true;
        IsAttacking = false;

        spriteObject = transform.GetChild(0).gameObject;
        animController = new AnimationNPCController(this, spriteObject.GetComponent<Animator>(), GetComponent<Animator>());
    }

    void FixedUpdate()
    {
        animController.Animate();
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
