using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private Player player;

    private Rigidbody2D entityBody;

    private void Start()
    {
        entityBody = player.Components.PlayersBody;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float playersVelocity = player.Stats.Velocity;
        Vector2 mousePos = Input.mousePosition;
        Vector2 playersPos = Camera.main.WorldToScreenPoint(entityBody.position);
        float angle = Mathf.Atan2(mousePos.y - playersPos.y, mousePos.x - playersPos.x) * Mathf.Rad2Deg;
        entityBody.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));


        Vector2Int movementDirection = new Vector2Int
        {
            x = (int)Mathf.Ceil(Input.GetAxis("Horizontal")),
            y = (int)Mathf.Ceil(Input.GetAxis("Vertical"))
        };

        entityBody.velocity = (Vector2)movementDirection * playersVelocity;

        player.IsMoving = entityBody.velocity != Vector2.zero;
    }
}
