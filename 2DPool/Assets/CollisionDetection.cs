using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    [SerializeField]
    private GameObject[] balls;

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
            if (!ball.GetComponent<BallMovement>().getAtPeace())
            {
                GameObject collider = CheckForCollision(ball);
                if (tableWalls.Contains(collider))
                {
                    CalculateHitWithTable(ball, tableWalls.IndexOf(collider));
                }
            }
        }
    }

    private void CalculateHitWithTable(GameObject ball, int wallNumber)
    {
        Vector3 directionOfBall = ball.GetComponent<BallMovement>().getDirection();
        Vector3 newDirection;
        if (wallNumber == 1 || wallNumber == 2)
        {
            newDirection = new Vector3(-directionOfBall.x, directionOfBall.y, directionOfBall.z);
        }
        else
        {
            newDirection = new Vector3(directionOfBall.x, -directionOfBall.y, directionOfBall.z);
        }
        ball.GetComponent<BallMovement>().SetDirection(newDirection);
    }

    private GameObject CheckForCollision(GameObject ball)
    {
        Vector3 pos = ball.transform.position;
        foreach (GameObject secondBall in balls)
        {
            if (Vector3.Distance(pos, secondBall.transform.position) < 1 && secondBall != ball)
            {
                return secondBall;
            }
        }
        
        for (int i = 0; i < 2; ++i)
        {
            if (CheckIfInsideTheTable(pos, walls[i, 1], walls[i, 0]))
            {
                return tableWalls[i];
            }

            //if (PointToLineDistance(pos,walls[i,0],walls[i,1]) <= 0.25)
            //{
            //    return tableWalls[i];
            //}
        }

        return null;
    }

    private float PointToLineDistance(Vector3 point, Vector3 lineStart, Vector3 lineEnd)
    {
        Vector3 firstVector = point - lineStart;
        Vector3 secondVector = lineEnd - lineStart;

        return Vector3.Cross(firstVector, secondVector).magnitude / secondVector.magnitude;
    }
    private bool CheckIfInsideTheTable(Vector3 point, Vector3 lineStart, Vector3 lineEnd)
    {

        //return Vector3.Cross(point - lineStart, lineEnd - lineStart) > 0;
        //return ((point.x - lineStart.x)*(lineEnd.y - lineStart.y) - (point.y - lineStart.y)*(lineEnd.x - lineStart.x)) > 0;
    }
}
