using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    private int money;

    void Start()
    {
        money = 1000;
        //BusinessEvents.current.OnBuyingProperty += setMoney;
        //BusinessEvents.current.OnUpgradingProperty += setMoney;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int getMoney()
    {
        return money;
    }

    public void DecreaseMoney(int number)
    {
        money -= number;
    }

    public void IncreaceMoney(int number)
    {
        money += number;
    }
}
