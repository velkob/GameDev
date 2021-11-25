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
        BallMovement ballMovement = whiteBall.GetComponent<BallMovement>();
        
        if (ballMovement.getAtPeace())
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            if (Input.GetMouseButton(0))
            {
                force += 0.01f;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                if (force > 0.5f)
                {
                    force = 0.5f;
                }
                ballMovement.SetVelocity(-(force / ballMovement.getMass()) * ((transform.position - whiteBall.transform.position).normalized));
                force = 0;
            }
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }

    }
}
