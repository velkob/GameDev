using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRunning : MonoBehaviour
{

    private float speed;
    private float jumpForce;
    private Rigidbody2D rb2d;
    private bool moveLeft;
    private bool reachedLeftMostPoint;


    [SerializeField]
    private LayerMask layerMask;

    public Vector2 velocity;

    void Start()
    {
        speed = 0;
        jumpForce = 0;
        rb2d = GetComponent<Rigidbody2D>();
        moveLeft = true;
        reachedLeftMostPoint = false;
    }

    void FixedUpdate()
    {
        Vector2 newPosition;
        if (moveLeft)
        {
            newPosition = new Vector2
            {
                x = -velocity.x * speed,
                y = isGrounded() ? velocity.y * jumpForce : velocity.y
            } * Time.deltaTime + rb2d.position;

        }
        else
        {
            newPosition = new Vector2
            {
                x = velocity.x * speed,
                y = isGrounded() ? velocity.y * jumpForce : velocity.y
            } * Time.deltaTime + rb2d.position;
        }
        
        rb2d.MovePosition(newPosition);

        /*if (moveLeft)
        {
            transform.position = new Vector3
            {
                x = transform.position.x - speed * Time.deltaTime,
                y = transform.position.y,
                z = transform.position.z
            };
        }
        else
        {
            transform.position = new Vector3
            {
                x = transform.position.x + speed * Time.deltaTime,
                y = transform.position.y,
                z = transform.position.z
            };
        }*/


        if (shouldTurn())
        {
            moveLeft = !moveLeft;
            transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
        }
    }

    private bool shouldTurn()
    {
        BoxCollider2D enemyBox = GetComponent<BoxCollider2D>();
        BoxCollider2D platformBox = transform.parent.GetComponent<BoxCollider2D>();
        if (reachedLeftMostPoint)
        {
            if (enemyBox.bounds.max.x > platformBox.bounds.max.x)
            {
                reachedLeftMostPoint = false;
            }
            return enemyBox.bounds.max.x > platformBox.bounds.max.x;
        }
        else
        {
            if (enemyBox.bounds.min.x < platformBox.bounds.min.x)
            {
                reachedLeftMostPoint = true;
            }
            return enemyBox.bounds.min.x < platformBox.bounds.min.x;
        }
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(GetComponent<BoxCollider2D>().bounds.center,
                                                  Vector2.down,
                                                  1f,
                                                  layerMask
                                                  );
        return raycastHit.collider != null;
    }

    public void setSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
    public void setJumpForce(float newJumpForce)
    {
        jumpForce = newJumpForce;
    }
}
