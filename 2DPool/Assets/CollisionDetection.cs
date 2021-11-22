using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    private float BALL_RADIUS = 0.25f;

    [SerializeField]
    private List<GameObject> balls;

    private List<GameObject> tableWalls;

    [SerializeField]
    private Vector2[] tableCorners;


    private void Start()
    {
        tableWalls = new List<GameObject>();
        for (int i = 0; i < 4; i++)
        {
            GameObject wall = new GameObject("wall" + i);
            tableWalls.Add(wall);
        }
    }
    private void FixedUpdate()
    {
        foreach (GameObject ball in balls)
        {
            BallMovement ballMovement = ball.GetComponent<BallMovement>();
          
            if (!ballMovement.getAtPeace())
            {
                GameObject collider = CheckForCollision(ball);

                if (collider == null)
                {
                    continue;
                }
                else if (tableWalls.Contains(collider))
                {
                    ballMovement.CalculateMovementAfterWallHit(tableWalls.IndexOf(collider));
                }
                else if (balls.Contains(collider))
                {
                    ballMovement.CalculateMovementAfterBallHit(collider);
                }
            }
        }


    }



    private GameObject CheckForCollision(GameObject ball)
    {
        Vector3 pos = ball.transform.position;
   
        if (pos.x - BALL_RADIUS < tableCorners[0].x)
        {
            ball.transform.position = new Vector3(tableCorners[0].x + BALL_RADIUS, pos.y, pos.z);
            return tableWalls[0];
        }
        if (pos.x + BALL_RADIUS > tableCorners[1].x)
        {
            ball.transform.position = new Vector3(tableCorners[1].x - BALL_RADIUS, pos.y, pos.z);
            return tableWalls[1];
        }
        if (pos.y - BALL_RADIUS < tableCorners[1].y)
        {
            ball.transform.position = new Vector3(pos.x, tableCorners[1].y + BALL_RADIUS, pos.z);
            return tableWalls[2];
        }
        if (pos.y + BALL_RADIUS > tableCorners[3].y)
        {
            ball.transform.position = new Vector3(pos.x, tableCorners[3].y - BALL_RADIUS, pos.z);
            return tableWalls[3];
        }

        foreach (GameObject secondBall in balls)
        {
            if (Vector3.Distance(pos, secondBall.transform.position) <= BALL_RADIUS * 2 && secondBall != ball)
            {
                Vector3 dist = ball.transform.position - secondBall.transform.position;
                Vector3 mtd = dist * ((BALL_RADIUS * 2 - dist.magnitude) / dist.magnitude);
                ball.transform.position += mtd / 2;
                secondBall.transform.position -= mtd / 2;
                return secondBall;
            }
        }

        return null;
    }

}
