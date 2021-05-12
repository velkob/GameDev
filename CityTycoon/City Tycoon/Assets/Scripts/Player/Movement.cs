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

    private RaycastHit turnTileRayCast;

    private void Start()
    {
        // direction = Vector3.forward;
        destination = transform.localPosition;
    }
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            destination = transform.localPosition + direction * Random.Range(1, 7);
        }
        transform.localPosition = Vector3.Lerp(transform.localPosition, destination, 0.9f*Time.deltaTime);

        if (Vector3.Distance(transform.localPosition,destination) < 0.0001f)
        {
            transform.localPosition = destination;
        }
        
        //transform.localPosition = destination;
        //if (CheckForTurn())
        //{
        //    Debug.Log(turnTileRayCast.transform.eulerAngles);
        //    Debug.Log(turnTileRayCast.transform.localEulerAngles);
        //}
    }

    //private bool CheckForTurn()
    //{
    //    Physics.Raycast(transform.position, Vector3.down,out turnTileRayCast,1);
    //    return turnTileRayCast.collider.gameObject == turnTile;
    //}
}
