using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJumping : MonoBehaviour
{

    private Rigidbody2D rigidbody2d;
    private float jumpForce;
    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private Vector2 velocity;

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        jumpForce = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrounded())
        {
            jump();
        }
    }

    private void jump()
    {
        Vector2 newPosition = new Vector2
        {
            x = velocity.x,
            y = velocity.y * jumpForce
        } * Time.deltaTime + rigidbody2d.position;

        rigidbody2d.MovePosition(newPosition);
    }

    bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(gameObject.GetComponent<BoxCollider2D>().bounds.center,
                                                  Vector2.down,
                                                  1f,
                                                  layerMask
                                                  );
        return raycastHit.collider != null;
    }

    public void setJumpForce(float newForce)
    {
        jumpForce = newForce;
    }
}
