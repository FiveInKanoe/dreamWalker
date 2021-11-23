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

    private Player player;

    public override void Initialize(Player player, GameObject skillContainer)
    {
        NextUsageTime = 0;
        SkillContainer = skillContainer;
        this.player = player;
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

            currentSword.transform.position = player.transform.position;
            currentSword.transform.rotation = player.transform.rotation;

            currentSword.GetComponent<Rigidbody2D>().velocity = new Vector2
                (
                Mathf.Cos(player.transform.rotation.eulerAngles.z * Mathf.Deg2Rad) * swordSpeed,
                Mathf.Sin(player.transform.rotation.eulerAngles.z * Mathf.Deg2Rad) * swordSpeed
                );
            Destroy(currentSword, lifeTime);
        }
    }

    
}
