using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocateObject : MonoBehaviour
{
    public GameObject GetObject()
    {
        bool hit;
        hit = Physics.BoxCast(transform.position,
            transform.localScale / 3,
            transform.forward,
            out RaycastHit raycastHit,
            transform.rotation,
            45);

        if (hit == true)
        {
            return raycastHit.collider.gameObject;
        }
        else
        {
            return null;
        }
    }

}
