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
    private GameObject forSaleSign;
    [SerializeField]
    private LayerMask layerMask;

    private void Start()
    {
        GameObject parent = new GameObject("Oxford Str");
        parent.transform.parent = transform;
        parent.transform.localScale = Vector3.one;
        GenerateStreet(parent);
        GenerateSaleSigns(parent);
    }

    private void GenerateStreet(GameObject parent)
    {
        for (int i = 0; i < 20; i++)
        {
            GameObject go = Instantiate(streetTile, Vector3.right * i, Quaternion.Euler(0, 0, 0), parent.transform);
            go.transform.localPosition = Vector3.right * i;
        }
    }

    private void GenerateSaleSigns(GameObject parent)
    {
        for (int i = 1; i < 20; i++)
        {
            Transform street = parent.transform.GetChild(i);
            GameObject go = Instantiate(forSaleSign,street.position + new Vector3(0,7,30),Quaternion.Euler(-90,180,0),transform);
        }
    }
}
