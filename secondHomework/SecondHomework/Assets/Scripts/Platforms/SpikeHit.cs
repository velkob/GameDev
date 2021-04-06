using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeHit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameEvents.current.Dying();
    }

}
