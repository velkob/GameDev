using System;
using System.Text.RegularExpressions;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    [SerializeField]
    GameObject[] Houses;

    [SerializeField]
    int[] prices;

    [SerializeField]
    int[] rents;

    public bool CheckIfUpgradable()
    {
        GameObject left, right;
        left = CheckLeftNeighbour();
        right = CheckRightNeighbour();
        PropertyInfo leftPropertyInfo = left != null ? left.GetComponent<PropertyInfo>() : null;
        PropertyInfo rightPropertyInfo = right != null ? right.GetComponent<PropertyInfo>() : null;
        if (left && right != null
            )
        {
            int houseTier = GetHouseTier(left) + GetHouseTier(right);
            if (houseTier + 1 <= Houses.Length)
            {
                return true;
            }
        }
        else if (right != null)
        {
            int houseTier = GetHouseTier(right);
            if (houseTier + 1 <= Houses.Length)
            {
                return true;
            }
        }
        else if (left != null)
        {
            int houseTier = GetHouseTier(left);
            if (houseTier + 1 <= Houses.Length)
            {
                return true;
            }
        }
        return false;
    }

    public int GetPrice()
    {
        GameObject player = GameObject.Find("TurnManager").GetComponent<TurnManagment>().GetCurrentPlayer();
        GameObject go = player.GetComponent<LocateObject>().GetObject();

        if (go.GetComponent<PropertyInfo>().Id == gameObject.GetComponent<PropertyInfo>().Id)
        {
            int houseTier = 0;
            GameObject left, right;
            left = CheckLeftNeighbour();
            right = CheckRightNeighbour();
            if (left && right != null)
            {
                houseTier = GetHouseTier(left) + GetHouseTier(right);
            }
            else if (right != null)
            {
                houseTier = GetHouseTier(right);

            }
            else if (left != null)
            {
                houseTier = GetHouseTier(left);
            }
            return prices[houseTier];
        }
        return -1;
    }

    public void UpgradeBuilding()
    {
        GameObject player = GameObject.Find("TurnManager").GetComponent<TurnManagment>().GetCurrentPlayer();
        GameObject go = player.GetComponent<LocateObject>().GetObject();

        if (go.GetComponent<PropertyInfo>().Id == gameObject.GetComponent<PropertyInfo>().Id)
        {
            GameObject left, right;
            left = CheckLeftNeighbour();
            right = CheckRightNeighbour();
            int houseTier;
            if (left && right != null)
            {
                houseTier = GetHouseTier(left) + GetHouseTier(right);
                left.SetActive(false);
                right.SetActive(false);
                gameObject.SetActive(false);
                CreateHouse(player, left, right, houseTier);
            }
            else if (right != null)
            {
                houseTier = GetHouseTier(right);
                right.SetActive(false);
                gameObject.SetActive(false);
                CreateHouse(player, right, houseTier);
            }
            else if (left != null)
            {
                houseTier = GetHouseTier(left);
                left.SetActive(false);
                gameObject.SetActive(false);
                CreateHouse(player, left, houseTier);
            }
        }
    }

    private void CreateHouse(GameObject player, GameObject neighbour, int houseTier)
    {
        GameObject house = Instantiate(Houses[houseTier],
                       GetNewHousePos(neighbour, houseTier),
                       Quaternion.Euler(0, -90, 0),
                       transform.parent);
        house.transform.localRotation = house.transform.rotation;
        house.transform.localPosition += Vector3.forward * houseTier * 0.02f;
        house.GetComponent<PropertyInfo>().PlayerID = player.GetComponent<PlayerInfo>().GetID();
        house.GetComponent<PropertyInfo>().Rent = rents[houseTier];
    }

    private void CreateHouse(GameObject player, GameObject left, GameObject right, int houseTier)
    {
        GameObject house = Instantiate(Houses[houseTier],
                       GetNewHousePos(left, right, houseTier),
                       Quaternion.Euler(0, -90, 0),
                       transform.parent);
        house.transform.localRotation = house.transform.rotation;
        //house.transform.localPosition += Vector3.back * houseTier * 0.1f;
        house.GetComponent<PropertyInfo>().PlayerID = player.GetComponent<PlayerInfo>().GetID();
        house.GetComponent<PropertyInfo>().Rent = rents[houseTier];
    }

    private int GetHouseTier(GameObject house)
    {
        return int.Parse(house.tag[1].ToString());
    }

    private Vector3 GetNewHousePos(GameObject house, int houseTier)
    {
        return Vector3.Lerp(
            new Vector3(transform.position.x, 0, transform.position.z),
            new Vector3(house.transform.position.x, 0, house.transform.position.z),
            0.5f);
    }

    private Vector3 GetNewHousePos(GameObject house1, GameObject house2, int houseTier)
    {
        Vector3 firstLerp = Vector3.Lerp(
            new Vector3(transform.position.x, 0, transform.position.z),
            new Vector3(house1.transform.position.x, 0, house1.transform.position.z),
            0.5f);

        Vector3 secondLerp = Vector3.Lerp(new Vector3(transform.position.x, 0, transform.position.z),
            new Vector3(house2.transform.position.x, 0, house2.transform.position.z),
            0.5f);

        return Vector3.Lerp(firstLerp, secondLerp, 0.5f);
    }

    bool hit;
    RaycastHit raycastHit;
    GameObject CheckLeftNeighbour()
    {
        hit = Physics.BoxCast(transform.position + transform.up, transform.localScale * 3, transform.forward, out raycastHit, transform.rotation, 30);
        string raycastHitTag = hit != false ? Regex.Match(raycastHit.collider.tag, "T\\dBuilding").Value : "DefinetlyNotATagName";
        raycastHitTag = string.IsNullOrEmpty(raycastHitTag) ? "DefinetlyNotATagName" : raycastHitTag;

        if (hit == true && raycastHit.collider.CompareTag(raycastHitTag))
        {
            return raycastHit.collider.gameObject;
        }
        else
        {
            return null;
        }
    }
    GameObject CheckRightNeighbour()
    {
        bool hit;
        hit = Physics.BoxCast(transform.position + transform.up, transform.localScale * 3, -transform.forward, out RaycastHit raycastHit, transform.rotation, 30);
        string raycastHitTag = hit != false ? Regex.Match(raycastHit.collider.tag, "T\\dBuilding").Value : "DefinetlyNotATagName";
        raycastHitTag = string.IsNullOrEmpty(raycastHitTag) ? "DefinetlyNotATagName" : raycastHitTag;

        if (hit == true && raycastHit.collider.CompareTag(raycastHitTag))
        {
            return raycastHit.collider.gameObject;
        }
        else
        {
            return null;
        }
    }
}
