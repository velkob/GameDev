using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocateObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public GameObject GetObject()
    {
        RaycastHit raycastHit;
        bool hit;
        hit = Physics.BoxCast(transform.position, transform.localScale * 3, Vector3.forward, out raycastHit, transform.rotation, 30);

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
