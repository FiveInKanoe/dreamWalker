using UnityEngine;

public class Ammo : MonoBehaviour
{
    public Transform ContainerTransform { get; set; }

    public void BeShooted(Vector3 origin, Quaternion rotation, float ammoVelocity, float ammoLifeTime)
    {
        Ammo currentSword = Instantiate(this, ContainerTransform);

        currentSword.transform.position = origin;
        currentSword.transform.rotation = rotation;

        currentSword.GetComponent<Rigidbody2D>().velocity = new Vector2
        {
            x = Mathf.Cos(rotation.eulerAngles.z * Mathf.Deg2Rad) * ammoVelocity,
            y = Mathf.Sin(rotation.eulerAngles.z * Mathf.Deg2Rad) * ammoVelocity
        };
        Destroy(currentSword.gameObject, ammoLifeTime);
    }
}
