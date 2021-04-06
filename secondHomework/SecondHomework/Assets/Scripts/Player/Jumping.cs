using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private LayerMask layerMask;
    private Rigidbody2D rigidbody2d;
    private BoxCollider2D boxCollider2d;
    private RaycastHit2D raycastHit;

    private bool doubleJump;

    void Awake()
    {
        doubleJump = true;
        rigidbody2d = transform.GetComponent<Rigidbody2D>();
        boxCollider2d = transform.GetComponent<BoxCollider2D>();
        GameEvents.current.OnJumpFromSpring += jumpingFromSpring;
    }
    void Update()
    {
        if (isGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            jump();
        }
        if (!isGrounded() && doubleJump == true && Input.GetKeyDown(KeyCode.Space))
        {
            jump();
            doubleJump = false;
        }
        if (isGrounded())
        {
            doubleJump = true;
        }

        showAnimation();

    }
    bool isGrounded()
    {
        raycastHit = Physics2D.BoxCast(boxCollider2d.bounds.center,
                                                  boxCollider2d.bounds.size,
                                                  0f,
                                                  Vector2.down,
                                                  1f,
                                                  layerMask
                                                  );
        return raycastHit.collider != null;
    }

    private void jump()
    {
        rigidbody2d.velocity = Vector2.up * 10f;
    }
    private void jumpingFromSpring()
    {
        rigidbody2d.velocity = Vector2.up * 15f;
    }

    private void showAnimation()
    {
        if (isGrounded())
        {
            animator.SetBool("inTheAir", false);
        }
        else
        {
            animator.SetBool("inTheAir", true);
        }
    }
    private void OnDestroy()
    {
        GameEvents.current.OnJumpFromSpring -= jumpingFromSpring;
    }
}
