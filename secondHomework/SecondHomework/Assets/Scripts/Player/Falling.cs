using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling : MonoBehaviour
{
    private float deathBoarder = -3f;

    private void Start()
    {
        GameEvents.current.OnDying += resetPosition;   
    }
    void Update()
    {
        if (transform.position.y <= deathBoarder)
        {
            GameEvents.current.Dying();
        }
    }

    private void resetPosition()
    {
        transform.position = new Vector3(0, 0, 0);
    }
    private void OnDestroy()
    {
        GameEvents.current.OnDying -= resetPosition;
    }
}
