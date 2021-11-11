using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> balls;

    private List<GameObject> tableWalls;

    [SerializeField]
    private Vector2[] tableCorners;

    private Vector2[,] walls;

    private void CalculateWalls()
    {
        int rows = 0, cols = 0;
        for (int i = 0; i < tableCorners.Length - 1; ++i)
        {
            for (int j = i + 1; j < tableCorners.Length; j++)
            {
                if (tableCorners[i].x == tableCorners[j].x)
                {
                    walls[rows, cols++] = tableCorners[i];
                    walls[rows, cols++] = tableCorners[j];

                    cols = 0;
                    rows++;
                }
                if (tableCorners[i].y == tableCorners[j].y)
                {
                    walls[rows, cols++] = tableCorners[i];
                    walls[rows, cols++] = tableCorners[j];

                    cols = 0;
                    rows++;
                }
            }
        }
    }


    private void Start()
    {
        tableWalls = new List<GameObject>();
        walls = new Vector2[4, 2];
        for (int i = 0; i < 4; i++)
        {
            GameObject wall = new GameObject("wall" + i);
            tableWalls.Add(wall);
        }
        CalculateWalls();
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
                    return;
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
        foreach (GameObject secondBall in balls)
        {
            if (Vector3.Distance(pos, secondBall.transform.position) <= 0.5 && secondBall != ball)
            {
                return secondBall;
            }
        }

        if (CheckIfInsideTheTable(pos))
        {
            for (int i = 0; i < 4; ++i)
            {
                if (PointToLineDistance(pos, walls[i, 0], walls[i, 1]) <= 0.25)
                {
                    return tableWalls[i];
                }
            }
        }
        else
        {
            //ball.transform.position = new Vector3(3, 5, -0.1f);
        }

        return null;
    }

    private float PointToLineDistance(Vector3 point, Vector3 lineStart, Vector3 lineEnd)
    {
        Vector3 firstVector = point - lineStart;
        Vector3 secondVector = lineEnd - lineStart;

        return Vector3.Cross(firstVector, secondVector).magnitude / secondVector.magnitude;
    }

    private bool CheckIfInsideTheTable(Vector3 point)
    {
        int counter = 0;
        for (int i = 1; i < 3; i++)
        {
            if (DoIntersect(point, point + new Vector3(20, 0, 0), walls[i, 0], walls[i, 1]))
            {
                counter++;
            }
        }
        return counter % 2 == 1;
    }

    private bool DoIntersect(Vector3 p1, Vector3 q1, Vector3 p2, Vector3 q2)
    {
        int o2 = Orientation(p1, q1, q2);
        int o3 = Orientation(p2, q2, p1);
        int o1 = Orientation(p1, q1, p2);
        int o4 = Orientation(p2, q2, q1);

        if (o1 != o2 && o3 != o4)
            return true;

        return false;
    }
    private int Orientation(Vector3 p, Vector3 q, Vector3 r)
    {
       float val = (q.y - p.y) * (r.x - q.x) -
                (q.x - p.x) * (r.y - q.y);

        if (val == 0) return 0; 

        return (val > 0) ? 1 : 2;
    }
}
