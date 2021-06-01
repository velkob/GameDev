using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropertyInfo : MonoBehaviour
{
    private int price;
    private static int GLOBAL_ID = 0;
    private int id;
    private int playerID;
    private int rent;

    public int Price { get => price; set => price = value; }
    public int Id { get => id; }
    public int PlayerID { get => playerID; set => playerID = value; }
    public int Rent { get => rent; set => rent = value; }

    private void Start()
    {
        price = 100;
        id = GLOBAL_ID++;
        playerID = -1;
    }
}
