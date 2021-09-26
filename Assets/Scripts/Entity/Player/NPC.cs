using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Entity
{
    private float viewAngle;

    public bool IsMoving { get; set; }
    public bool IsAttacking { get; set; }

    private AnimationNPCController animController;

    private GameObject spriteObject;

    void Start()
    {
        IsMoving = false;
        IsAttacking = false;

        spriteObject = transform.GetChild(0).gameObject;
        animController = new AnimationNPCController(this, spriteObject.GetComponent<Animator>());

    }

    void FixedUpdate()
    {

        viewAngle = transform.rotation.eulerAngles.z;
        animController.Animate(viewAngle);

    }

    private void LateUpdate()
    {
        transform.GetChild(0).rotation = Quaternion.identity;
    }
}
