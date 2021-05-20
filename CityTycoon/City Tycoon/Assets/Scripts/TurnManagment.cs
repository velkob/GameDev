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
        Debug.Log(player.GetComponent<LocateObject>().GetObject().tag);
        //if the building is ForSale sign
        if (buildingOnTheLeft.CompareTag("ForSale"))
        {
            BusinessEvents.current.ForSaleStep();
        }

        //Waits for player to Choose an option
        //offer buttons call EndTurn();
    }

    public void EndTurn()
    {
        SaleOffer.SetActive(false);
        StartTurn();
    }

    private IEnumerator WaitForPlayerToStop()
    {
        while(player.GetComponent<Movement>().isMoving)
        {
            Debug.Log(1);
            yield return null;
        }
        Debug.Log(1);
        AfterMovementEvent();

    }
}
