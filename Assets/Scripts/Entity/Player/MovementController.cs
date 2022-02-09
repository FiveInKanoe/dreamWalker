using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private Player player;

    private Rigidbody2D entityBody;

    void Start()
    {
        entityBody = player.Manager.PlayersBody;
    }

    private void FixedUpdate()
    {
        //Input.GetAxis("Horizontal") == 1     Input.GetAxisRaw("Vertical") == 1  
        float playersSpeed = player.Stats.Velocity;
        Vector2 mousePos = Input.mousePosition;
        Vector2 playersPos = Camera.main.WorldToScreenPoint(entityBody.position);
        float angle = Mathf.Atan2(mousePos.y - playersPos.y, mousePos.x - playersPos.x) * Mathf.Rad2Deg;
        entityBody.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        if (Input.GetKey(KeyCode.W))
        {
            entityBody.velocity = new Vector2(0, playersSpeed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            entityBody.velocity = new Vector2(0, -playersSpeed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            entityBody.velocity = new Vector2(-playersSpeed, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            entityBody.velocity = new Vector2(playersSpeed, 0);
        }
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
        {
            entityBody.velocity = new Vector2(playersSpeed, playersSpeed);
        }
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
        {
            entityBody.velocity = new Vector2(-playersSpeed, playersSpeed);
        }
        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
        {
            entityBody.velocity = new Vector2(playersSpeed, -playersSpeed);
        }
        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
        {
            entityBody.velocity = new Vector2(-playersSpeed, -playersSpeed);
        }

        bool isMoving = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||
                        Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);
        bool conflictX = Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D);
        bool conflictY = Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S);


        if (!isMoving || conflictX || conflictY)
        {
            entityBody.velocity = new Vector2(0, 0);
            isMoving = false;
        }
        player.IsMoving = isMoving;
    }
}
