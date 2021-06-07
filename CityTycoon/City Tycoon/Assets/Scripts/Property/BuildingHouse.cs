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
                Quaternion.Euler(0, -90, 0),
                transform.parent);
            house.transform.localRotation = house.transform.rotation;
            house.GetComponent<PropertyInfo>().PlayerID = player.GetComponent<PlayerInfo>().GetID();
            house.GetComponent<PropertyInfo>().Rent = 10;
            PaintTheRooftop(house);
            gameObject.SetActive(false);
        };
    }

    private void PaintTheRooftop(GameObject house)
    {
        Material[] materials = house.transform.GetChild(0).GetComponent<Renderer>().materials;
        GameObject player = GameObject.Find("TurnManager").GetComponent<TurnManagment>().GetCurrentPlayer();
        foreach (Material mat in materials)
        {
            if (mat.name == "Roof (Instance)")
            {
                Debug.Log(player.GetComponent<PlayerInfo>().GetPlayerColor());
                mat.color = player.GetComponent<PlayerInfo>().GetPlayerColor();
            }
        }
    }
}
