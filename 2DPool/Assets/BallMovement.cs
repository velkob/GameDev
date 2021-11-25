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
    private Vector2 velocity;

    [SerializeField]
    private bool atPeace;

    [SerializeField]
    private Vector3 whiteballStartingPos;

    private const float VELOCITY_INCREMENT = 0.0005f;
    private const float VELOCITY_CAP = 0.001f;

    void Start()
    {
        velocity = new Vector2(0, 0);
        atPeace = true;
    }

    void FixedUpdate()
    {
        if (velocity.x != 0 || velocity.y != 0)
        {
            atPeace = false;
            transform.position += new Vector3(velocity.x, velocity.y, 0);
            if (velocity.x != 0)
            {
                velocity.x += velocity.x > 0 ? -VELOCITY_INCREMENT : VELOCITY_INCREMENT;
            }
            if (velocity.y != 0)
            {
                velocity.y += velocity.y > 0 ? -VELOCITY_INCREMENT : VELOCITY_INCREMENT;
            }
            if ((velocity.x < VELOCITY_CAP && velocity.x > 0) || (velocity.x > -VELOCITY_CAP && velocity.x < 0))
            {
                velocity.x = 0;
            }
            if ((velocity.y < VELOCITY_CAP && velocity.y > 0) || (velocity.y > -VELOCITY_CAP && velocity.y < 0))
            {
                velocity.y = 0;
            }
        }
        if (velocity.x == 0 && velocity.y == 0)
        {
            atPeace = true;
        }
    }

    public void CalculateMovementAfterWallHit(int wallNumber)
    {
        if (wallNumber == 0 || wallNumber == 1)
        {
            velocity.x = -velocity.x;
        }
        else
        {
            velocity.y = -velocity.y;
        }

        velocity *= elasticity;
    }

    public void CalculateMovementAfterBallHit(GameObject secondBall)
    {
        BallMovement secondBallMovement = secondBall.GetComponent<BallMovement>();
        
        Vector3 pos = transform.position;
        Vector3 secondBallPos = secondBall.transform.position;
        Vector3 velocitySecondBall = secondBallMovement.GetVelocity();
        float massSecondBall = secondBallMovement.getMass();
        
        Vector2 normalVector = new Vector2(secondBallPos.x - pos.x, secondBallPos.y - pos.y);
        Vector2 unitNormalVector = normalVector / normalVector.magnitude;
        Vector2 unitTangentVector = new Vector2(-unitNormalVector.y, unitNormalVector.x);

        float v1n = Vector2.Dot(unitNormalVector, velocity);
        float v1t = Vector2.Dot(unitTangentVector, velocity);
        float v2n = Vector2.Dot(unitNormalVector, velocitySecondBall);
        float v2t = Vector2.Dot(unitTangentVector, velocitySecondBall);

        float finalVelocity1 = (v1n * (mass - massSecondBall) + 2 * massSecondBall * v2n) / (mass + massSecondBall);
        float finalVelocity2 = (v2n * (massSecondBall - mass) + 2 * mass * v1n) / (mass + massSecondBall);
        
        Vector2 v1nTang = finalVelocity1 * unitNormalVector;
        Vector2 v2nTang = finalVelocity2 * unitNormalVector;

        Vector2 v1tTang = unitTangentVector * v1t;
        Vector2 v2tTang = unitTangentVector * v2t;

        velocity = (v1nTang + v1tTang)*elasticity;
        secondBallMovement.SetVelocity((v2nTang + v2tTang)*secondBallMovement.getElasticity());


    }

    public void ResetWhiteBall()
    {
        transform.position = whiteballStartingPos;
        velocity = new Vector2(0, 0);
    }

    public bool getAtPeace()
    {
        return atPeace;
    }

    public float getElasticity()
    {
        return elasticity;
    }

    public float getMass()
    {
        return mass;
    }

    public void SetVelocity(Vector3 newVelocity)
    {
        velocity = newVelocity;
    }

    public Vector3 GetVelocity()
    {
        return velocity;
    }
}
