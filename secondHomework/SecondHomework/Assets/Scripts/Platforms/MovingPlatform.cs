using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float maxLeft;
    public float maxRight;
    private bool direction; // true for rigth false for left

    void Start()
    {
        maxLeft = transform.position.x - 1;
        maxRight = transform.position.x + 1;

    }
    void Update()
    {
        if (direction == true)
        {
            transform.position = transform.position + Vector3.right * Time.deltaTime;
        }
        else
        {
            transform.position = transform.position + Vector3.left * Time.deltaTime;
        }


        if (transform.position.x > maxRight)
        {
            direction = false;
        }
        else if(transform.position.x < maxLeft)
        {
            direction = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.collider.transform.SetParent(transform);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.collider.transform.SetParent(null);
    }
}
