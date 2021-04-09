using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Running : MonoBehaviour
{
    public Animator animator;
    private float speed = 5;

    void Update()
    {
        float input = Input.GetAxis("Horizontal");

        animator.SetFloat("Horizontal", input);
        
        Vector3 horizontal = new Vector3(input, 0.0f, 0.0f);
        transform.position = transform.position + horizontal * speed * Time.deltaTime;

        if (input >= 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        } 
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
