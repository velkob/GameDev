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
        GameObject parent = new GameObject("Oxford Str");
        parent.transform.parent = transform;
        parent.transform.localScale = Vector3.one;
        //GenerateStreet(parent);
    }

    private void GenerateStreet(GameObject parent)
    {
        for (int i = 0; i < 20; i++)
        {
            GameObject go = Instantiate(streetTile, Vector3.right * i, Quaternion.Euler(0, 0, 0), parent.transform);
            go.transform.localPosition = Vector3.right * i;
        }
    }
}
