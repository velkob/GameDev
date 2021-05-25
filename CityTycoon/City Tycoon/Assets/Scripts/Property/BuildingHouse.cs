using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingHouse : MonoBehaviour
{
    [SerializeField]
    private GameObject house1;

    [SerializeField]
    private int price;
    void Start()
    {
        //BusinessEvents.current.OnBuyingProperty += BuildAHouse;
    }

    public void BuildAHouse()
    {
        GameObject buildingOnTheLeft = GameObject.Find("Player").GetComponent<LocateObject>().GetObject();
        if (buildingOnTheLeft.CompareTag("ForSale") && buildingOnTheLeft.GetComponent<PropertyInfo>().getId() == gameObject.GetComponent<PropertyInfo>().getId())
        {
            Instantiate(house1, new Vector3(transform.position.x, 0, transform.position.z), Quaternion.Euler(0, 0, 0), transform.parent);
            gameObject.SetActive(false);
        };
    }

    private void OnDestroy()
    {
        //BusinessEvents.current.OnBuyingProperty -= BuildAHouse;
    }
}
