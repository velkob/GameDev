using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct Street
{

    public Vector3 position;
    public Vector3 direction;
    public Vector3 prevDirection;

    public Street(Vector3 _position, Vector3 _direction, Vector3 _prevDirection)
    {
        position = _position;
        direction = _direction;
        prevDirection = _prevDirection;
    }
}
public class StreetGeneration : MonoBehaviour
{
    private const int TILESIZE = 4;
    private Vector3[] directions = new Vector3[4] { Vector3.back, Vector3.forward, Vector3.left, Vector3.right };
    private string[] streetNames = {"Oxford Str", "Piccadilly Str", "Fifth Avenue Str",
                                    "Tsarigradsko Shose Str", "Arbat Str", "Hollywood Str",
                                    "Wall Str", "Graben Str", "Broadway Str", "Champs-Élysées Str"};

    [SerializeField]
    private int streetCount;
    [SerializeField]
    private GameObject streetTile;
    [SerializeField]
    private GameObject turnTile;
    [SerializeField]
    private LayerMask layerMask;

    void Start()
    {
        Street street = new Street(new Vector3(500, 0, 500), Vector3.one, Vector3.one);
        for (int i = 0; i < streetCount; i++)
        {
            GameObject streetObj = new GameObject(streetNames[UnityEngine.Random.Range(0, streetNames.Length)]);
            streetObj.transform.parent = transform;

            street = GenerateStreet(street, UnityEngine.Random.Range(10, 40), streetObj);
        }
    }

    private Street GenerateStreet(Street street, int numberOfTiles, GameObject parent)
    {
        Vector3 direction = ChooseDirection(street.direction, street.prevDirection);
        for (int i = 0; i < numberOfTiles; i++)
        {
            GameObject go = Instantiate(streetTile,
                             street.position + direction * i * TILESIZE,
                             Quaternion.Euler(0, 0, 0),
                             parent.transform);
            if (CheckForStreetAhead(go, direction))
            {
                if (CheckForCrossRoad(go))
                {

                }
                PlaceTurnTile(street.position + direction * ++i * TILESIZE, direction, parent);
                return new Street(street.position, direction, street.direction);
            }
        }

        Vector3 endPosition = street.position + direction * numberOfTiles * TILESIZE;

        return new Street(endPosition, direction, Vector3.one);
    }

    private bool CheckForCrossRoad(GameObject go)
    {
        bool result = true;
        foreach (Vector3 direction in directions)
        {
            result = result && Physics.Raycast(go.transform.position, direction, TILESIZE + 1, layerMask);
        }

        return result;
    }

    private Vector3 ChooseDirection(Vector3 previousDirection, Vector3 penultimateDirection = new Vector3())
    {
        List<Vector3> tempDirections = new List<Vector3>();
        foreach (Vector3 direction in directions)
        {
            if (previousDirection != (direction * -1) && penultimateDirection != (direction * -1))
            {
                tempDirections.Add(direction);
            }
        }

        return tempDirections[UnityEngine.Random.Range(0, tempDirections.Count)];
    }

    private void PlaceTurnTile(Vector3 position, Vector3 direction, GameObject parent)
    {
        Instantiate(turnTile, position, Quaternion.Euler(0, 0, 0), parent.transform);
    }

    private bool CheckForStreetAhead(GameObject go, Vector3 direction)
    {
        return Physics.Raycast(go.transform.position, direction, TILESIZE + 1, layerMask);
    }

    //private void populateArrays()
    //{
    //    streetLength = new int[streetCount] { 30, 40, 20, 15, 15, 35, 30, 10, 10, 25 };
    //    streetCoords = new Vector3[streetCount] {
    //        new Vector3(0,0,160),
    //        new Vector3(0,0,0),
    //        new Vector3(60,0,80),
    //        new Vector3(120,0,100),
    //        new Vector3(60,0,100),
    //        new Vector3(0,0,80),
    //        new Vector3(140,0,-40),
    //        new Vector3(0,0,0),
    //        new Vector3(40,0,-40),
    //        new Vector3(40,0,-40)
    //    };
    //    direction = new Direction[streetCount] { 
    //        Direction.Vertical,
    //        Direction.Horizontal,
    //        Direction.Horizontal,
    //        Direction.Horizontal,
    //        Direction.Vertical,
    //        Direction.Vertical,
    //        Direction.Horizontal,
    //        Direction.Vertical,
    //        Direction.Horizontal,
    //        Direction.Vertical,
    //    };
    //    turns = new int[streetCount][];

    //}
}
