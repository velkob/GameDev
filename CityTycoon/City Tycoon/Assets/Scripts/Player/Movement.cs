using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private Vector3 direction;

    [SerializeField]
    private GameObject turnTile;

    private Vector3[] destination;

    private int tilesMoved;

    public bool isMoving;

    private void Start()
    {
        // direction = Vector3.forward;
        destination = new Vector3[6];
    }
    void Update()
    {

        if (isMoving)
        {
            for (int i = 0; i < tilesMoved; i++)
            {
                isMoving = true;
                transform.localPosition = Move(destination[i]).GetEnumerator().Current;
            }
        }

        //transform.localPosition = destination;
        //if (CheckForTurn())
        //{
        //    Debug.Log(turnTileRayCast.transform.eulerAngles);
        //    Debug.Log(turnTileRayCast.transform.localEulerAngles);
        //}
    }

    public void StartMoving()
    {
        isMoving = true;
        tilesMoved = Random.Range(3, 3);
        //destination = transform.localPosition + direction * tilesMoved;
        for (int i = 0; i < tilesMoved; i++)
        {
            destination[i] = transform.localPosition + (i+1) * direction;
        }
    }

    private IEnumerable<Vector3> Move(Vector3 destination)
    {
        while (isMoving)
        {
            if (Vector3.Distance(transform.localPosition, destination) < 0.01f)
            {
                transform.localPosition = destination;
                isMoving = false;
            }
            yield return Vector3.Lerp(transform.localPosition, destination, 0.0001f);
        }
    }

    RaycastHit turnTileRayCast;
    bool hit;
    private bool CheckForTurn()
    {
        hit = Physics.Raycast(transform.localPosition, Vector3.down, out turnTileRayCast, 1);

        return turnTileRayCast.collider.gameObject == turnTile;
    }
}
