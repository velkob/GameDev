using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocateObject : MonoBehaviour
{
    public GameObject GetObject()
    {
        RaycastHit raycastHit;
        bool hit;
        hit = Physics.BoxCast(transform.position, transform.localScale * 3, transform.forward, out raycastHit, transform.rotation, 30);

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
