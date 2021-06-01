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
    private GameObject Camera;

    [SerializeField]
    private GameObject[] players;

    private GameObject currentPlayer;

    private int playersIndex;

    void Start()
    {
        playersIndex = 0;
        StartTurn();
    }

    public void StartTurn()
    {
        currentPlayer = players[playersIndex];
        Camera.GetComponent<CameraFollowing>().player = currentPlayer.transform;
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
        currentPlayer.GetComponent<Movement>().StartMoving();
        //waits for movement to finish and calls AfterMovementEvent
        StartCoroutine(WaitForPlayerToStop());
    }

    public void AfterMovementEvent()
    {
        //Locate whats left of the player
        GameObject buildingOnTheLeft = currentPlayer.GetComponent<LocateObject>().GetObject();
        PlayerInfo playerInfo = currentPlayer.GetComponent<PlayerInfo>();
        PropertyInfo propertyInfo = buildingOnTheLeft.GetComponent<PropertyInfo>();

        if (propertyInfo.PlayerID != -1 && playerInfo.GetID() != propertyInfo.PlayerID)
        {
            playerInfo.DecreaseMoney(propertyInfo.Rent);
        }
        else if (propertyInfo.PlayerID == -1 || playerInfo.GetID() == propertyInfo.PlayerID)
        {
            if (buildingOnTheLeft.CompareTag("ForSale")) //if the building is ForSale sign
            {
                SaleOffer.SetActive(true);
                StartCoroutine(WaitForPlayerToChooseForSale()); //Waits for player to choose an option
            }
            else if (buildingOnTheLeft.CompareTag("Bank"))
            {
                currentPlayer.GetComponent<PlayerInfo>().IncreaceMoney(1000);
                EndTurn();
            }
        }
    }



    public void EndTurn()
    {
        SaleOffer.SetActive(false);
        UpgradeOffer.SetActive(false);
        playersIndex = playersIndex + 1 == players.Length ? 0 : playersIndex + 1;
        
        Invoke(nameof(StartTurn), 0);
    }

    private IEnumerator WaitForPlayerToStop()
    {
        while (currentPlayer.GetComponent<Movement>().isMoving)
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

        GameObject buildingOnTheLeft = currentPlayer.GetComponent<LocateObject>().GetObject();
        if (buildingOnTheLeft.GetComponent<Upgrade>().CheckIfUpgradable() &&
            buildingOnTheLeft.GetComponent<Upgrade>().GetPrice() < currentPlayer.GetComponent<PlayerInfo>().getMoney() &&
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
        GameObject buildingOnTheLeft = currentPlayer.GetComponent<LocateObject>().GetObject();
        int price = buildingOnTheLeft.GetComponent<PropertyInfo>().Price;
        if (price > currentPlayer.GetComponent<PlayerInfo>().getMoney())
        {
            InsufficentFunds.SetActive(true);
            return;
        }
        buildingOnTheLeft.GetComponent<BuildingHouse>().BuildAHouse();
        currentPlayer.GetComponent<PlayerInfo>().DecreaseMoney(price);
    }

    public void UpgradeHouse()
    {
        GameObject buildingOnTheLeft = currentPlayer.GetComponent<LocateObject>().GetObject();
        int price = buildingOnTheLeft.GetComponent<Upgrade>().GetPrice();
        if (price > currentPlayer.GetComponent<PlayerInfo>().getMoney())
        {
            InsufficentFunds.SetActive(true);
            return;
        }
        buildingOnTheLeft.GetComponent<Upgrade>().UpgradeBuilding();
        currentPlayer.GetComponent<PlayerInfo>().DecreaseMoney(price);
    }

    public GameObject GetCurrentPlayer()
    {
        return currentPlayer;
    }
}
