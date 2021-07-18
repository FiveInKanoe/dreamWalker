using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementControl
{
    public void Move(Rigidbody2D playersBody, Collider2D playersCollider, float playersSpeed)
    {
        Vector2 mousePos = Input.mousePosition;
        Vector2 playersPos = Camera.main.WorldToScreenPoint(playersBody.position);
        float angle = Mathf.Atan2(mousePos.y - playersPos.y, mousePos.x - playersPos.x) * Mathf.Rad2Deg;
        playersBody.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        if (Input.GetKey(KeyCode.W) == true)
        {       
            playersBody.velocity = new Vector2(0, playersSpeed);
        }
        if (Input.GetKey(KeyCode.S) == true)
        {
            playersBody.velocity = new Vector2(0, -playersSpeed);
        }
        if (Input.GetKey(KeyCode.A) == true)
        {
            playersBody.velocity = new Vector2(-playersSpeed, 0);
        }
        if (Input.GetKey(KeyCode.D) == true)
        {
            playersBody.velocity = new Vector2(playersSpeed, 0);
        }

        if (Input.GetKey(KeyCode.W) == true && Input.GetKey(KeyCode.D) == true)
        {
            playersBody.velocity = new Vector2(playersSpeed, playersSpeed);
        }
        if (Input.GetKey(KeyCode.W) == true && Input.GetKey(KeyCode.A) == true)
        {
            playersBody.velocity = new Vector2(-playersSpeed, playersSpeed);
        }
        if (Input.GetKey(KeyCode.S) == true && Input.GetKey(KeyCode.D) == true)
        {
            playersBody.velocity = new Vector2(playersSpeed, -playersSpeed);
        }
        if (Input.GetKey(KeyCode.S) == true && Input.GetKey(KeyCode.A) == true)
        {
            playersBody.velocity = new Vector2(-playersSpeed, -playersSpeed);
        }


        bool isMoving = Input.GetKey(KeyCode.W) == true || Input.GetKey(KeyCode.A) == true ||
                        Input.GetKey(KeyCode.S) == true || Input.GetKey(KeyCode.D) == true;

        bool conflictX = Input.GetKey(KeyCode.A) == true && Input.GetKey(KeyCode.D) == true;
        bool conflictY = Input.GetKey(KeyCode.W) == true && Input.GetKey(KeyCode.S) == true;

        if (!isMoving || conflictX || conflictY)
        {
            playersBody.velocity = new Vector2(0, 0);
        }

    }
}
