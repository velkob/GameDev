using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{

    [SerializeField]
    private float mass;

    [SerializeField]
    private float velocity;

    [SerializeField]
    private Vector3 direction;
 
    private bool atPeace;

    void Start()
    {
        //velocity = 0;
        atPeace = true;
        mass = 8;
    }

    void FixedUpdate()
    {
        if (velocity > 0)
        {
            atPeace = false;
            velocity -= 0.01f;
            transform.position += direction * velocity;
        }
        if (velocity <= 0.0001f)
        {
            velocity = 0;
            atPeace = true;
        }
    }

    public void Accelerate(float force, Vector3 direction)
    {
        velocity += force / mass;
        this.direction = direction;
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
}
