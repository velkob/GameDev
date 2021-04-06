using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;
    private Vector3 position;
    private GameObject current;

    void Awake()
    {
        rigidbody2d = transform.GetComponent<Rigidbody2D>();
        position = transform.position;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Tim"))
        {
            Invoke("dropPlatform", 0.5f);
            Invoke("removePlatform", 3f);
            Invoke("ressurectPlatform", 6f);
        }
    }

    void dropPlatform()
    {
        rigidbody2d.isKinematic = false;
    }
    void removePlatform()
    {
        gameObject.SetActive(false);
    }
    void ressurectPlatform()
    {
        transform.position = position;
        rigidbody2d.isKinematic = true;
        gameObject.SetActive(true);
    }
}
