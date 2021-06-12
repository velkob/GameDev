using System.Collections;
using UnityEngine;

public class TurnManagment : MonoBehaviour
{
    [SerializeField]
    private GameObject UIRollButton;

    [SerializeField]
    private GameObject UISaleOffer;

    [SerializeField]
    private GameObject UIUpgradeOffer;

    [SerializeField]
    private GameObject UIInsufficentFunds;

    [SerializeField]
    private GameObject UIPowerUpPlacement;

    [SerializeField]
    private GameObject Camera;

    [SerializeField]
    private GameObject[] players;

    [SerializeField]
    private GameObject[] powerups;

    private GameObject currentPlayer;

    private int playersIndex;

    void Start()
    {
        playersIndex = 0;
        StartTurn();
    }

    private void StartTurn()
    {
        currentPlayer = players[playersIndex];
        Camera.GetComponent<CameraFollowing>().player = currentPlayer.transform;
        RollDice();
    }

    private void RollDice()
    {
        UIRollButton.SetActive(true);
        //rollButton calls MovePlayer();
    }

    private void MovePlayer()
    {
        UIRollButton.SetActive(false);

        // calls Movement.Move()
        currentPlayer.GetComponent<Movement>().StartMoving();
        //waits for movement to finish and calls AfterMovementEvent
        StartCoroutine(WaitForPlayerToStop());
    }

    private void AfterMovementEvent()
    {
        //Locate whats left of the player
        GameObject buildingOnTheLeft = currentPlayer.GetComponent<LocateObject>().GetObject();
        PlayerInfo playerInfo = currentPlayer.GetComponent<PlayerInfo>();
        PropertyInfo propertyInfo = buildingOnTheLeft.GetComponent<PropertyInfo>();

        if (propertyInfo.PlayerID == -1)
        {
            if (buildingOnTheLeft.CompareTag("ForSale")) //if the building is ForSale sign
            {
                UISaleOffer.SetActive(true);
                StartCoroutine(WaitForPlayerToChooseForSale()); //Waits for player to choose an option
            }
            else if (buildingOnTheLeft.CompareTag("Bank"))
            {
                currentPlayer.GetComponent<PlayerInfo>().IncreaceMoney(1000);
                PowerUpPlacement();
            }
            else if (buildingOnTheLeft.CompareTag("SuperMarket"))
            {
                currentPlayer.GetComponent<PowerUps>().OwnedPowerUp = powerups[Random.Range(0, powerups.Length)];
                PowerUpPlacement();
            }
        }
        else if (propertyInfo.PlayerID != -1 && playerInfo.GetID() != propertyInfo.PlayerID)
        {
            playerInfo.DecreaseMoney(propertyInfo.Rent);
            IncreasePropertyOwnerMoney(propertyInfo.PlayerID, propertyInfo.Rent);
            PowerUpPlacement();
        }
        else if (playerInfo.GetID() == propertyInfo.PlayerID)
        {
            PowerUpPlacement();
        }
    }

    private void PowerUpPlacement()
    {
        if (currentPlayer.GetComponent<PowerUps>().OwnedPowerUp != null)
        {
            UIPowerUpPlacement.SetActive(true);
        }
        StartCoroutine(WaitForPlayerToPlacePowerUp());
    }

    private IEnumerator WaitForPlayerToPlacePowerUp()
    {
        while (UIPowerUpPlacement.activeSelf)
        {
            yield return null;
        }

        EndTurn();
    }

    private void EndTurn()
    {
        UISaleOffer.SetActive(false);
        UIUpgradeOffer.SetActive(false);
        UIPowerUpPlacement.SetActive(false);
        playersIndex = playersIndex + 1 == players.Length ? 0 : playersIndex + 1;

        Invoke(nameof(StartTurn), 0);
    }

    private void IncreasePropertyOwnerMoney(int ownerID, int rent)
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            PlayerInfo playerInfo = player.GetComponent<PlayerInfo>();
            if (playerInfo.GetID() == ownerID)
            {
                playerInfo.IncreaceMoney(rent);
            }
        }
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
        while (UISaleOffer.activeSelf)
        {
            yield return null;
        }

        GameObject buildingOnTheLeft = currentPlayer.GetComponent<LocateObject>().GetObject();
        if (buildingOnTheLeft.GetComponent<Upgrade>().CheckIfUpgradable() &&
            buildingOnTheLeft.GetComponent<Upgrade>().GetPrice() < currentPlayer.GetComponent<PlayerInfo>().getMoney() &&
            !buildingOnTheLeft.CompareTag("ForSale")) // check if the building has neighbours that belong to the same player and can be upgraded
        {
            UIUpgradeOffer.SetActive(true); //Waits for player to Choose an option
        }

        while (UIUpgradeOffer.activeSelf || UIInsufficentFunds.activeSelf)
        {
            yield return null;
        }

        PowerUpPlacement();
    }

    public void BuildHouse()
    {
        GameObject buildingOnTheLeft = currentPlayer.GetComponent<LocateObject>().GetObject();
        int price = buildingOnTheLeft.GetComponent<PropertyInfo>().Price;
        if (price > currentPlayer.GetComponent<PlayerInfo>().getMoney())
        {
            UIInsufficentFunds.SetActive(true);
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
            UIInsufficentFunds.SetActive(true);
            return;
        }
        buildingOnTheLeft.GetComponent<Upgrade>().UpgradeBuilding();
        currentPlayer.GetComponent<PlayerInfo>().DecreaseMoney(price);
    }

    public void PlacePowerUP()
    {
        currentPlayer.GetComponent<PowerUps>().PlacePowerUp();
    }

    public GameObject GetCurrentPlayer()
    {
        return currentPlayer;
    }
}
