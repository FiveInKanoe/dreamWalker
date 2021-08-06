using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingSword : Skill
{
    [SerializeField] private GameObject swordAmmo;
    [SerializeField] private float lifeTime;
    [SerializeField] private float damage;
    [SerializeField] private float swordSpeed;
    [SerializeField] private int maxSwordsCount;

    private GameObject flyingSwordCont;

    new void Start()
    {
        base.Start();
        flyingSwordCont = new GameObject("FlyingSword");
        flyingSwordCont.transform.SetParent(skillContainer.transform);
    }

    public override void Usage()
    {

        if (flyingSwordCont.transform.childCount < maxSwordsCount)
        {
            GameObject currentSword = Instantiate(swordAmmo, flyingSwordCont.transform);

            currentSword.transform.position = transform.position;
            currentSword.transform.rotation = transform.rotation;

            currentSword.GetComponent<Rigidbody2D>().velocity = new Vector2
                (
                Mathf.Cos(transform.rotation.eulerAngles.z * Mathf.Deg2Rad) * swordSpeed,
                Mathf.Sin(transform.rotation.eulerAngles.z * Mathf.Deg2Rad) * swordSpeed
                );
            Destroy(currentSword, lifeTime);
        }
    }

}
