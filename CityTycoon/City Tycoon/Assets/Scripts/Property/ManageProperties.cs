using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageProperties : MonoBehaviour
{
    [SerializeField]
    private GameObject forSaleSign;

    [SerializeField]
    private GameObject bank;

    void Start()
    {
        GenerateProperties(GameObject.Find("Streets").transform.GetChild(0).gameObject);
    }
    private void GenerateProperties(GameObject parent)
    {
        for (int i = 1; i < 20; i++)
        {
            if (i != 6)
            {
                Transform street = parent.transform.GetChild(i);
                GameObject go = Instantiate(forSaleSign,
                    street.position + new Vector3(0, 7, 30),
                    Quaternion.Euler(-90, 180, 0),
                    transform);
                go.name = "ForSale" + i;
            }
            else
            {
                Transform street = parent.transform.GetChild(i);
                GameObject go = Instantiate(bank,
                    street.position + new Vector3(0, 0, 30),
                    Quaternion.Euler(0, 0, 0),
                    transform);
                go.name = "Bank";
            }
        }
    }
}
