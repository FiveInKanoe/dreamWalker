using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementControl
{
    private Rigidbody2D playersBody;
    private float playersSpeed;

    public MovementControl(Rigidbody2D playersBody, float playersSpeed)
    {
        this.playersBody = playersBody;
        this.playersSpeed = playersSpeed;
    }

    public void Move()
    {
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
