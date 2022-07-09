using UnityEngine;

[CreateAssetMenu(fileName = "RangerClass", menuName = "Player Classes/Ranger Class")]
public class RangerControl : ClassControl
{
    [SerializeField] private GameObject ammo;
    [SerializeField] private float ammoLifeTime;
    [SerializeField] private float ammoDelayTime;
    [SerializeField] private float ammoVelocity;
    private float endOfDelay;

    private GameObject ammoContainer;

    private void OnEnable()
    {
        ammoContainer = null;
        ClassType = PlayerClass.RANGER;
        endOfDelay = 0;
    }

    public override void Initialize(Player player)
    {
        base.Initialize(player);
        ammoContainer = new GameObject("Players Ammo Container");
    }

    public override void Control()
    {
        if (Input.GetMouseButton(0) && Time.time > endOfDelay)
        {
            PerformAttack();
            Player.IsAttacking = true;
            endOfDelay = Time.time + ammoDelayTime;
        }
        else
        {
            Player.IsAttacking = false;
        }
    }

    private void PerformAttack()
    {
        GameObject currentAmmo = Instantiate(ammo, ammoContainer.transform);

        currentAmmo.transform.position = Player.transform.position;
        currentAmmo.transform.rotation = Player.transform.rotation;

        currentAmmo.GetComponent<Rigidbody2D>().velocity = new Vector2
            (
            Mathf.Cos(Player.transform.rotation.eulerAngles.z * Mathf.Deg2Rad) * ammoVelocity,
            Mathf.Sin(Player.transform.rotation.eulerAngles.z * Mathf.Deg2Rad) * ammoVelocity
            );
        Destroy(currentAmmo, ammoLifeTime);
    }

    
}
