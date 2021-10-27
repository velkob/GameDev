using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{

    [SerializeField]
    private float mass;

    [SerializeField]
    private float acceleration;

    [SerializeField]
    private Vector3 direction;
 
    private bool atPeace;

    void Start()
    {
        //acceleration = 0;
        atPeace = true;
        mass = 8;
    }

    void FixedUpdate()
    {
        if (acceleration > 0)
        {
            atPeace = false;
            acceleration -= 0.01f;
            transform.position += direction * acceleration;
        }
    }

    public void Accelerate(float force, Vector3 direction)
    {
        acceleration = force / mass;
        this.direction = direction;
    }
}
