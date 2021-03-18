using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupKey : MonoBehaviour
{
     public int id;

    private void Start()
    {
        GameEvents.current.OnKeyPickUp += onKeyPickUp;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameEvents.current.keyPickUp(id);
    }
    private void onKeyPickUp(int id)
    {
        if (id == this.id)
        {
            Destroy(gameObject);
            GameEvents.current.OnKeyPickUp -= onKeyPickUp;
        }
    }
}
