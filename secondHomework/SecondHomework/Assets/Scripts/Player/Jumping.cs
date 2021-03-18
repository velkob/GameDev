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
    
    void Awake()
    {
        rigidbody2d = transform.GetComponent<Rigidbody2D>();
        boxCollider2d = transform.GetComponent<BoxCollider2D>();
        GameEvents.current.OnJumpFromSpring += jumpingFromSpring;
    }
    void Update()
    {
        if (isGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody2d.velocity = Vector2.up * 10f;
        }

        if (isGrounded())
        {
            animator.SetBool("inTheAir", false);
        }
        else
        {
            animator.SetBool("inTheAir", true);
        }
    }
    bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2d.bounds.center,
                                                  boxCollider2d.bounds.size,
                                                  0f,
                                                  Vector2.down,
                                                  0.1f, 
                                                  layerMask
                                                  );
        return raycastHit.collider != null;
    }
    private void jumpingFromSpring()
    {
        rigidbody2d.velocity = Vector2.up * 15f;
    }

    private void OnDestroy()
    {
        GameEvents.current.OnJumpFromSpring -= jumpingFromSpring;
    }
}
