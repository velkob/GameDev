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

    private const float velocityIncrement = 0.0005f;
    private const float velocityCap = 0.001f;

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
                velocity.x += velocity.x > 0 ? -velocityIncrement : velocityIncrement;
            }
            if (velocity.y != 0)
            {
                velocity.y += velocity.y > 0 ? -velocityIncrement : velocityIncrement;
            }
            if ((velocity.x < velocityCap && velocity.x > 0) || (velocity.x > -velocityCap && velocity.x < 0))
            {
                velocity.x = 0;
            }
            if ((velocity.y < velocityCap && velocity.y > 0) || (velocity.y > -velocityCap && velocity.y < 0))
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
        
        Vector2 normalVector = new Vector2(secondBallPos.x - pos.x, secondBallPos.y - pos.y);
        Vector2 unitNormalVector = normalVector / normalVector.magnitude;
        Vector2 unitTangentVector = new Vector2(-unitNormalVector.y, unitNormalVector.x);

        float v1n = Vector2.Dot(unitNormalVector, velocity);
        float v1t = Vector2.Dot(unitTangentVector, velocity);
        float v2n = Vector2.Dot(unitNormalVector, velocitySecondBall);
        float v2t = Vector2.Dot(unitTangentVector, velocitySecondBall);

        Vector2 v1nTang = v2n * unitNormalVector;
        Vector2 v2nTang = v1n * unitNormalVector;

        Vector2 v1tTang = unitTangentVector * v1t;
        Vector2 v2tTang = unitTangentVector * v2t;

        velocity = v1nTang + v1tTang;
        velocity *= elasticity;
        secondBallMovement.SetVelocity((v2nTang + v2tTang)*secondBallMovement.getElasticity());

        //float finalVelocity1 = (v1n * (mass - massSecondBall) + 2 * massSecondBall * v2n) / mass + massSecondBall;
        //float finalVelocity2 = (v2n * (massSecondBall - mass) + 2 * mass * v1n) / mass + massSecondBall;

        //float power = (Math.Abs(pos.x) + Math.Abs(pos.y)) + (Math.Abs(secondBallPos.x) + Math.Abs(secondBallPos.y));
        //power *= 0.00482f;
        //float opposite = -(pos.y - secondBallPos.y);
        //float adjacent = -(pos.x - secondBallPos.x);
        //float rotation = Mathf.Atan2(opposite, adjacent) * Mathf.Rad2Deg;

        //BallMovement secondBallMovement = secondBall.GetComponent<BallMovement>();
        //secondBallMovement.SetAtPeace(false);
        //direction += new Vector3(90 * (float)Math.Cos(rotation + Math.PI) * power, 90 * (float)Math.Sin(rotation + Math.PI) * power, velocity.z).normalized;
        //velocity *= 0.95f;
        //speed /= 2;


        //secondBallMovement.SetSpeedAndDirection(speed,(secondBallMovement.GetVelocity() +
        //    (new Vector3(90 * (float)Math.Cos(rotation) * power,
        //    90 * (float)Math.Sin(rotation) * power,
        //    velocity.z)).normalized));

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
