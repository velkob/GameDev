using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Sign
{
    Move10Tiles,
    Stop,
    TurnAround
}
public class PowerUpInfo : MonoBehaviour
{
    private Sign sign;
    private void Start()
    {
        if (name == "Stop(Clone)")
        {
            sign = Sign.Stop;
        }
        else if (name == "TurnAround(Clone)")
        {
            sign = Sign.TurnAround;
        }
        else if (name == "Move10Tiles(Clone)")
        {
            sign = Sign.Move10Tiles;
        }
    }

    public Sign GetSign()
    {
        return sign;
    }
}
