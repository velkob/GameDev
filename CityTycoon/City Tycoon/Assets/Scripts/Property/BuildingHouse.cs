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

    public void BuildAHouse()
    {
        GameObject player = GameObject.Find("TurnManager").GetComponent<TurnManagment>().GetCurrentPlayer();
        GameObject buildingOnTheLeft = player.GetComponent<LocateObject>().GetObject();
        if (buildingOnTheLeft.CompareTag("ForSale") &&
            buildingOnTheLeft.GetComponent<PropertyInfo>().Id == gameObject.GetComponent<PropertyInfo>().Id)
        {
            GameObject house = Instantiate(house1,
                new Vector3(transform.position.x, 0, transform.position.z),
                Quaternion.Euler(0, 0, 0),
                transform.parent);
            house.GetComponent<PropertyInfo>().PlayerID = player.GetComponent<PlayerInfo>().GetID();
            gameObject.SetActive(false);
        };
    }
}
