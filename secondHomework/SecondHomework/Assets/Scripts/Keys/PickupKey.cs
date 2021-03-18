using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupKey : MonoBehaviour
{
     public int id;

    private void Start()
    {
        GameEvents.current.onKeyPickUp += onKeyPickUp;
    }

    private void onKeyPickUp(int id)
    {
        if (id == this.id)
        {
            Destroy(gameObject);
            GameEvents.current.onKeyPickUp -= onKeyPickUp;
        }
    }
}
