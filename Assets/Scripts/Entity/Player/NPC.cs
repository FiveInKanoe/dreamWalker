using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Entity
{
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

        // viewAngle = transform.rotation.eulerAngles.z;
        animController.Animate();
        // Debug.Log(anima);
    }

    private void LateUpdate()
    {
        transform.GetChild(0).rotation = Quaternion.identity;
    }
}
