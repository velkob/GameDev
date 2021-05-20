using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyingProperty : MonoBehaviour
{
    private int money;
    public int properyLayer;
    public Movement playerMovement;
    private bool locatedProperty = false;
    [SerializeField]
    private GameObject saleOffer;

    private void Start()
    {
        //BusinessEvents.current.OnBuyingProperty += spendMoney;
    }
    private void LateUpdate()
    {
        if (!playerMovement.isMoving && !locatedProperty && saleOffer.activeSelf == false)
        {
            GameObject property = LocatePropery();
            if (property != null && property.CompareTag("ForSale"))
            {
                locatedProperty = true;
                BusinessEvents.current.ForSaleStep();
            }
        }
        locatedProperty = false;
    }
    public GameObject LocatePropery()
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

    private void spendMoney()
    {
        int amout = LocatePropery().GetComponent<PropertyInfo>().getPrice();
        if (money < amout)
        {
            //TODO: How to stop an event from happening
        }
        else
        {
            money -= amout;
        }
    }
}
