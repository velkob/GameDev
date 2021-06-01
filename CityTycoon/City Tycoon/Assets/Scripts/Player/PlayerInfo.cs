using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    private int money;
    private int id;
    private static int GLOBAL_ID = 0;

    void Start()
    {
        money = 1000;
        id = GLOBAL_ID++;
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
    
    public int GetID()
    {
        return id;
    }
}
