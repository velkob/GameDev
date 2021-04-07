using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupKey : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameEvents.current.keyPickUp();
        Destroy(gameObject);
    }
}
