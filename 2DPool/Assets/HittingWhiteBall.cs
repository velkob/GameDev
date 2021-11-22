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
        if (Input.GetMouseButton(0) && whiteBall.GetComponent<BallMovement>().getAtPeace())
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            force += 0.01f;
        }
        else if (Input.GetMouseButtonUp(0) && whiteBall.GetComponent<BallMovement>().getAtPeace())
        {
            BallMovement ballMovement = whiteBall.GetComponent<BallMovement>();
            if (force > 0.5f)
            {
                force = 0.5f;
            }
            ballMovement.SetVelocity(-(force / ballMovement.getMass())*((transform.position - whiteBall.transform.position).normalized));
            force = 0;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }

    }
}
