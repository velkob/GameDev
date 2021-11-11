using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{

    [SerializeField]
    private float mass;

    [SerializeField]
    private float elasticity;

    [SerializeField]
    private float speed;

    [SerializeField]
    private Vector3 direction;

    private bool atPeace;

    void Start()
    {
        atPeace = true;
        mass = 1;
    }

    void FixedUpdate()
    {
        if (speed > 0)
        {
            atPeace = false;
            speed -= 0.005f;
            transform.position += direction * speed;
        }
        if (speed <= 0.0001f)
        {
            speed = 0;
            atPeace = true;
        }
    }

    public void CalculateMovementAfterWallHit(int wallNumber)
    {
        Vector3 newDirection;
        if (wallNumber == 1 || wallNumber == 2)
        {
            newDirection = new Vector3(-direction.x, direction.y, direction.z);
        }
        else
        {
            newDirection = new Vector3(direction.x, -direction.y, direction.z);
        }

        float newSpeed = speed * elasticity;

        SetSpeedAndDirection(newSpeed, newDirection);
    }

    public void SetSpeedAndDirection(float newSpeed, Vector3 newDirection)
    {
        speed = newSpeed;
        direction = newDirection;
    }

    public void SetDirection(Vector3 newDirection)
    {
        direction = newDirection;
    }

    public bool getAtPeace()
    {
        return atPeace;
    }

    public Vector3 getDirection()
    {
        return direction;
    }

    public float getElasticity()
    {
        return elasticity;
    }

    public float getSpeed()
    {
        return speed;
    }

    public float getMass()
    {
        return mass;
    }

    internal void CalculateMovementAfterBallHit(GameObject collider)
    {

    }
}
