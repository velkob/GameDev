using System;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    private int money;
    private int id;
    private static int GLOBAL_ID = 0;
    private Color playerColor;

    void Start()
    {
        GameOver.current.PlayerLoosesAction += DestroyObject;
        money = 2500;
        id = GLOBAL_ID++;
        FindAndSetColor();

    }

    private void FindAndSetColor()
    {
        Material[] materials = transform.GetComponent<Renderer>().materials;
        foreach (Material mat in materials)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(mat.name, "Car_Paint_(Red|Blue|Green|Yellow)"))
            {
                playerColor = mat.color;
            }
        }
    }

    public int getMoney()
    {
        return money;
    }

    public void DecreaseMoney(int number)
    {
        money -= number;
        if (money <= 0)
        {
            GameOver.current.PlayerLooses(id);
        }
    }

    public void IncreaceMoney(int number)
    {
        money += number;
    }

    public int GetID()
    {
        return id;
    }

    public Color GetPlayerColor()
    {
        return playerColor;
    }
    private void DestroyObject(int id)
    {
        if (this.id == id)
        {
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        GameOver.current.PlayerLoosesAction -= DestroyObject;
    }
}
