using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FlyingSword", menuName = "Skills/Flying Sword")]
public class FlyingSword : Skills
{
    [SerializeField] private GameObject swordAmmo;
    [SerializeField] private float lifeTime;
    [SerializeField] private float damage;
    [SerializeField] private float swordSpeed;
    [SerializeField] private int maxSwordsCount;

    private GameObject flyingSwordCont;

    private GameObject entity;

    public override void Init(GameObject gameObject, GameObject skillContainer)
    {
        NextUsageTime = 0;
        this.SkillContainer = skillContainer;
        entity = gameObject;
        flyingSwordCont = new GameObject("FlyingSword");
        flyingSwordCont.transform.SetParent(this.SkillContainer.transform);
    }

    public override void Usage()
    {
        if (Input.GetKey(HotKey) && Time.time > NextUsageTime)
        {
            Perform();
            NextUsageTime = Time.time + CoolDown;
        }
    }

    private void Perform()
    {

        if (flyingSwordCont.transform.childCount < maxSwordsCount)
        {
            GameObject currentSword = Instantiate(swordAmmo, flyingSwordCont.transform);

            currentSword.transform.position = entity.transform.position;
            currentSword.transform.rotation = entity.transform.rotation;

            currentSword.GetComponent<Rigidbody2D>().velocity = new Vector2
                (
                Mathf.Cos(entity.transform.rotation.eulerAngles.z * Mathf.Deg2Rad) * swordSpeed,
                Mathf.Sin(entity.transform.rotation.eulerAngles.z * Mathf.Deg2Rad) * swordSpeed
                );
            Destroy(currentSword, lifeTime);
        }
    }

    
}
