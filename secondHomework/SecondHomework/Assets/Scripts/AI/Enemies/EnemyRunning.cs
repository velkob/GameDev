using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRunning : MonoBehaviour
{

    private float speed;
    private Rigidbody2D rb2d;
    private bool moveLeft;
    private bool reachedLeftMostPoint;

    [SerializeField]
    private LayerMask layerMask;

    public Vector2 velocity;

    void Start()
    {
        speed = 0;
        rb2d = GetComponent<Rigidbody2D>();
        moveLeft = true;
    }

    void Update()
    {
        Vector2 newPosition;
        if (moveLeft)
        { 
            newPosition = new Vector2
            {
                x = -velocity.x * speed,
                y = velocity.y
            } * Time.deltaTime + rb2d.position;
        }
        else
        {
            newPosition = new Vector2
            {
                x = velocity.x * speed,
                y = velocity.y
            } * Time.deltaTime + rb2d.position;
        }

        rb2d.MovePosition(newPosition);


        if (shouldTurn())
        {
            moveLeft = !moveLeft;
        }
    }

    private bool shouldTurn()
    {
        BoxCollider2D enemyBox = gameObject.GetComponent<BoxCollider2D>();
        BoxCollider2D platformBox = gameObject.transform.parent.GetComponent<BoxCollider2D>();
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

    public void setSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

}
