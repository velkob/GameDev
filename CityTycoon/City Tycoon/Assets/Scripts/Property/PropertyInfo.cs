using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropertyInfo : MonoBehaviour
{
    private int price;
    private static int GLOBAL_ID = 0;
    private int id;

    private void Start()
    {
        price = 100;
        id = GLOBAL_ID++;
    }

    public int getPrice()
    {
        return price;
    }

    public int getId()
    {
        return id;
    }
}
