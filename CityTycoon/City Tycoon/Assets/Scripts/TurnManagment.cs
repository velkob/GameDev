using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManagment : MonoBehaviour
{
    [SerializeField]
    private GameObject rollButton;

    [SerializeField]
    private GameObject SaleOffer;

    [SerializeField]
    private GameObject UpgradeOffer;

    [SerializeField]
    private GameObject player;

    void Start()
    {
        StartTurn();
    }

    public void StartTurn()
    {
        RollDice();
    }

    public void RollDice()
    {
        rollButton.SetActive(true);

        //rollButton calls MovePlayer();
    }

    public void MovePlayer()
    {
        rollButton.SetActive(false);

        // calls Movement.Move()
        player.GetComponent<Movement>().StartMoving();
        //waits for movement to finish and calls AfterMovementEvent
        StartCoroutine(WaitForPlayerToStop());
    }

    public void AfterMovementEvent()
    {
        //Locate whats left of the player
        GameObject buildingOnTheLeft = player.GetComponent<LocateObject>().GetObject();

        if (buildingOnTheLeft.CompareTag("ForSale")) //if the building is ForSale sign
        {
            SaleOffer.SetActive(true);
            StartCoroutine(WaitForPlayerToChooseForSale(buildingOnTheLeft)); //Waits for player to choose an option
        }
    }



    public void EndTurn()
    {
        SaleOffer.SetActive(false);
        UpgradeOffer.SetActive(false);
        StartTurn();
    }

    private IEnumerator WaitForPlayerToStop()
    {
        while (player.GetComponent<Movement>().isMoving)
        {
            yield return null;
        }
        AfterMovementEvent();

    }

    private IEnumerator WaitForPlayerToChooseForSale(GameObject buildingOnTheLeft)
    {
        while (SaleOffer.activeSelf)
        {
            yield return null;
        }

        if (buildingOnTheLeft.GetComponent<Upgrade>().CheckIfUpgradable()) // check if the building has neighbours that belong to the same player and can be upgraded
        {
            UpgradeOffer.SetActive(true); //Waits for player to Choose an option
        }

        while (UpgradeOffer.activeSelf)
        {
            yield return null;
        }

        EndTurn();
    }

    public void BuildHouse()
    {
        GameObject buildingOnTheLeft = player.GetComponent<LocateObject>().GetObject();
        buildingOnTheLeft.GetComponent<BuildingHouse>().BuildAHouse();
        player.GetComponent<PlayerInfo>().DecreaseMoney(buildingOnTheLeft.GetComponent<PropertyInfo>().getPrice());
    }

    public void UpgradeHouse()
    {
        GameObject buildingOnTheLeft = player.GetComponent<LocateObject>().GetObject();
        int price = buildingOnTheLeft.GetComponent<Upgrade>().UpgradeBuilding();
        player.GetComponent<PlayerInfo>().DecreaseMoney(price);
    }
}
