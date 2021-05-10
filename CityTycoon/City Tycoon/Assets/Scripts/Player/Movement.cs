using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private Vector3 direction;

    private Vector3 destination;

    private void Start()
    {
        // direction = Vector3.forward;
        destination = transform.position;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // rigidbody.velocity = direction*moveDistance*Time.deltaTime;
            destination = transform.position + direction * ((Random.value * 10) % 6);
            Debug.Log(direction * ((Random.value * 10) % 6));
        }
        transform.position = Vector3.Lerp(transform.position, destination, speed*Time.deltaTime);
    }
}
