using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HittingWhiteBall : MonoBehaviour
{
    [SerializeField]
    private GameObject whiteBall;

    private float force;
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            force += 0.01f;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            whiteBall.GetComponent<BallMovement>().Accelerate(force,-(transform.position - whiteBall.transform.position));
            force = 0;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }

    }
}
