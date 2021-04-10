using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJumping : MonoBehaviour
{

    private Rigidbody2D rigidbody2d;
    private float jumpForce;
    [SerializeField]
    private LayerMask layerMask;

    void Start()
    {
        rigidbody2d = transform.GetComponent<Rigidbody2D>();
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
        rigidbody2d.velocity = Vector2.up * jumpForce * 10f;
        // transform.position = new Vector3(transform.position.x, transform.position.y + jumpForce, transform.position.z);
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
