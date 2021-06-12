using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropertyInfo : MonoBehaviour
{
    //private int price;
    private static int GLOBAL_ID = 0;
    //private int id;
    //private int playerID;
    //private int rent;

    public int Price { get; set; }
    public int Id { get; set; }
    public int PlayerID { get; set; } = -1;
    public int Rent { get; set; }

    private void Start()
    {
        GameOver.current.PlayerLoosesAction += DestroyHouses;
        GameOver.current.PlayerLoosesAction += RestoreForSaleSigns;
        Price = 100;
        Id = GLOBAL_ID++;
    }

    private void DestroyHouses(int id)
    {
        if (id == PlayerID && !CompareTag("ForSale"))
        {
            Destroy(gameObject);
        }
    }
    private void RestoreForSaleSigns(int id)
    {
        if (id == PlayerID && CompareTag("ForSale"))
        {
            gameObject.SetActive(true);
        }
    }
    private void OnDestroy()
    {
        GameOver.current.PlayerLoosesAction -= DestroyHouses;
        GameOver.current.PlayerLoosesAction -= RestoreForSaleSigns;
    }
}
