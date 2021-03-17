using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;

    void Awake()
    {
        rigidbody2d = transform.GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Tim"))
        {
            Invoke("dropPlatform", 0.5f);
            Destroy(gameObject, 3f);
        }
    }

    void dropPlatform()
    {
        rigidbody2d.isKinematic = false;
    }
}
