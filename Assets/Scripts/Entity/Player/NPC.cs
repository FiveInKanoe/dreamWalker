using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Entity
{

    //private int attackRadius = 100;

    private AnimationNPCController animController;

    private GameObject spriteObject;

    public Vector3 PosCurrentWP { get; set; }
    public float AttackRadius { get; set; }

    void Start()
    {
        Stats.IsMoving = true;
        Stats.IsAttacking = false;

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
