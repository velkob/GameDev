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
    private GameObject InsufficentFunds;

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
            StartCoroutine(WaitForPlayerToChooseForSale()); //Waits for player to choose an option
        }
        else if (buildingOnTheLeft.CompareTag("Bank"))
        {
            player.GetComponent<PlayerInfo>().IncreaceMoney(1000);
            EndTurn();
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

    private IEnumerator WaitForPlayerToChooseForSale()
    {
        while (SaleOffer.activeSelf)
        {
            yield return null;
        }

        GameObject buildingOnTheLeft = player.GetComponent<LocateObject>().GetObject();
        if (buildingOnTheLeft.GetComponent<Upgrade>().CheckIfUpgradable() &&
            buildingOnTheLeft.GetComponent<Upgrade>().GetPrice() < player.GetComponent<PlayerInfo>().getMoney() &&
            !buildingOnTheLeft.CompareTag("ForSale")) // check if the building has neighbours that belong to the same player and can be upgraded
        {
            UpgradeOffer.SetActive(true); //Waits for player to Choose an option
        }

        while (UpgradeOffer.activeSelf || InsufficentFunds.activeSelf)
        {
            yield return null;
        }

        EndTurn();
    }

    public void BuildHouse()
    {
        GameObject buildingOnTheLeft = player.GetComponent<LocateObject>().GetObject();
        int price = buildingOnTheLeft.GetComponent<PropertyInfo>().getPrice();
        if (price > player.GetComponent<PlayerInfo>().getMoney())
        {
            InsufficentFunds.SetActive(true);
            return;
        }
        buildingOnTheLeft.GetComponent<BuildingHouse>().BuildAHouse();
        player.GetComponent<PlayerInfo>().DecreaseMoney(price);
    }

    public void UpgradeHouse()
    {
        GameObject buildingOnTheLeft = player.GetComponent<LocateObject>().GetObject();
        int price = buildingOnTheLeft.GetComponent<Upgrade>().GetPrice();
        if (price > player.GetComponent<PlayerInfo>().getMoney())
        {
            InsufficentFunds.SetActive(true);
            return;
        }
        buildingOnTheLeft.GetComponent<Upgrade>().UpgradeBuilding();
        player.GetComponent<PlayerInfo>().DecreaseMoney(price);
    }
}
