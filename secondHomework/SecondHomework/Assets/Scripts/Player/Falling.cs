using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling : MonoBehaviour
{
    private float deathBoarder = -3f;
    
     void Update()
    {
        if (transform.position.y <= deathBoarder)
        {
            GameEvents.current.fallingToDeath();
            transform.position = new Vector3(0, 0, 0);
        }
    }
}
