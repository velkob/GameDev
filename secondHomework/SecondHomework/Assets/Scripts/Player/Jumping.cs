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
    }
    void Update()
    {
        if (isGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            //animator.SetFloat("Vertical", Input.GetAxis("Vertical"));
            rigidbody2d.velocity = Vector2.up * 10f;
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
}
