using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSceneStreetGen : MonoBehaviour
{
    [SerializeField]
    private int streetCount;
    [SerializeField]
    private GameObject streetTile;
    [SerializeField]
    private GameObject turnTile;
    [SerializeField]
    private LayerMask layerMask;

    private void Awake()
    {
        //GameObject parent = new GameObject("Oxford Str");
        //parent.transform.parent = transform;
        //parent.transform.localScale = Vector3.one;
        //GenerateStreet(parent);
    }

    private void GenerateStreet(GameObject parent)
    {
        for (int i = 0; i < 20; i++)
        {
            GameObject street = Instantiate(streetTile, Vector3.right * i, Quaternion.Euler(0, 0, 0), parent.transform);
            street.transform.localPosition = Vector3.right * i;
        }
        GameObject turn = Instantiate(turnTile, Vector3.right * 20, Quaternion.Euler(0, 270, 0), parent.transform);
        turn.transform.localPosition = Vector3.right * 20;
    }
}
