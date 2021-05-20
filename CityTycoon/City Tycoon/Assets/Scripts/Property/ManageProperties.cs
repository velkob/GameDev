using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageProperties : MonoBehaviour
{
    [SerializeField]
    private GameObject forSaleSign;

    void Start()
    {
        GenerateSaleSigns(GameObject.Find("Streets").transform.GetChild(0).gameObject);
    }
    private void GenerateSaleSigns(GameObject parent)
    {
        for (int i = 1; i < 20; i++)
        {
            Transform street = parent.transform.GetChild(i);
            GameObject go = Instantiate(forSaleSign,
                street.position + new Vector3(0, 7, 30),
                Quaternion.Euler(-90, 180, 0),
                transform);
            go.name = "ForSale" + i;
        }
    }
}
