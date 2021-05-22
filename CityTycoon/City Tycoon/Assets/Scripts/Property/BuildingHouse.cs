using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingHouse : MonoBehaviour
{
    [SerializeField]
    private GameObject house1;

    void Start()
    {
        BusinessEvents.current.OnBuyingProperty += BuildAHouse;
    }

    private void BuildAHouse()
    {
        GameObject go = GameObject.Find("Player").GetComponent<LocateObject>().GetObject();
        go.GetInstanceID();
        if (go.CompareTag("ForSale") && go.GetComponent<PropertyInfo>().getId() == gameObject.GetComponent<PropertyInfo>().getId())
        {
            Instantiate(house1, new Vector3(transform.position.x, 0, transform.position.z), Quaternion.Euler(0, 0, 0), transform.parent);
            gameObject.SetActive(false);
        };
    }

    private void OnDestroy()
    {
        BusinessEvents.current.OnBuyingProperty -= BuildAHouse;
    }
}
