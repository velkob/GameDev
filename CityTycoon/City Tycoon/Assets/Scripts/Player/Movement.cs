using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private Vector3 direction;

    [SerializeField]
    private GameObject turnTile;

    private Vector3 destination;

    public bool isMoving;

    private void Start()
    {
        // direction = Vector3.forward;
        destination = transform.localPosition;
    }
    void Update()
    {

        if (isMoving)
        {
            Move();
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
        destination = transform.localPosition + direction * Random.Range(1, 1);
    }

    private void Move()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, destination, 0.9f * Time.deltaTime);
        if (Vector3.Distance(transform.localPosition, destination) < 0.01f)
        {
            transform.localPosition = destination;
            isMoving = false;
        }
    }

    
    //private bool CheckForTurn()
    //{
    //    Physics.Raycast(transform.position, Vector3.down,out turnTileRayCast,1);
    //    return turnTileRayCast.collider.gameObject == turnTile;
    //}
}
