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
        GameObject left = CheckLeftNeighbour();
        GameObject right = CheckRightNeighbour();
        PlayerInfo playerInfo = GameObject.Find("TurnManager")
                                            .GetComponent<TurnManagment>()
                                            .GetCurrentPlayer()
                                            .GetComponent<PlayerInfo>();
        PropertyInfo leftPropertyInfo = left != null ? left.GetComponent<PropertyInfo>() : null;
        PropertyInfo rightPropertyInfo = right != null ? right.GetComponent<PropertyInfo>() : null;
        if (left && right != null)
        {
            if (playerInfo.GetID() != leftPropertyInfo.PlayerID && playerInfo.GetID() != rightPropertyInfo.PlayerID)
            {
                return false;
            }
            int houseTier = GetHouseTier(left) + GetHouseTier(right);
            if (houseTier + 1 <= Houses.Length)
            {
                return true;
            }
        }
        else if (right != null)
        {
            if (playerInfo.GetID() != rightPropertyInfo.PlayerID)
            {
                return false;
            }
            int houseTier = GetHouseTier(right);
            if (houseTier + 1 <= Houses.Length)
            {
                return true;
            }
        }
        else if (left != null)
        {
            if (playerInfo.GetID() != leftPropertyInfo.PlayerID)
            {
                return false;
            }
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
            int playerID = player.GetComponent<PlayerInfo>().GetID();
            PropertyInfo leftPropertyInfo = left != null ? left.GetComponent<PropertyInfo>() : null;
            PropertyInfo rightPropertyInfo = right != null ? right.GetComponent<PropertyInfo>() : null;

            if (left && right != null)
            {
                if (playerID == leftPropertyInfo.PlayerID && playerID == rightPropertyInfo.PlayerID)
                {
                    CreateHouse(playerID, left, right);
                }
                else if (playerID != leftPropertyInfo.PlayerID && playerID == rightPropertyInfo.PlayerID)
                {
                    CreateHouse(playerID, right);
                }
                else if (playerID == leftPropertyInfo.PlayerID && playerID != rightPropertyInfo.PlayerID)
                {
                    CreateHouse(playerID, left);
                }
            }
            else if (right != null)
            {
                CreateHouse(playerID, right);
            }
            else if (left != null)
            {
                CreateHouse(playerID, left);
            }
        }
    }

    private void CreateHouse(int playerID, GameObject neighbour)
    {
        int houseTier = GetHouseTier(neighbour);
        neighbour.SetActive(false);
        gameObject.SetActive(false);

        GameObject house = Instantiate(Houses[houseTier],
                       GetNewHousePos(neighbour, houseTier),
                       Quaternion.Euler(0, -90, 0),
                       transform.parent);
        house.transform.localRotation = house.transform.rotation;
        house.transform.localPosition += Vector3.forward * houseTier * 0.02f;
        house.GetComponent<PropertyInfo>().PlayerID = playerID;
        house.GetComponent<PropertyInfo>().Rent = rents[houseTier];
        PaintTheRooftop(house);
    }

    private void PaintTheRooftop(GameObject house)
    {
        Material[] materials = house.transform.GetChild(0).GetComponent<Renderer>().materials;
        GameObject player = GameObject.Find("TurnManager").GetComponent<TurnManagment>().GetCurrentPlayer();
        foreach (Material mat in materials)
        {
            if (mat.name == "Roof (Instance)")
            {
                Debug.Log(player.GetComponent<PlayerInfo>().GetPlayerColor());
                mat.color = player.GetComponent<PlayerInfo>().GetPlayerColor();
            }
        }
    }

    private void CreateHouse(int playerID, GameObject left, GameObject right)
    {
        int houseTier = GetHouseTier(left) + GetHouseTier(right);
        left.SetActive(false);
        right.SetActive(false);
        gameObject.SetActive(false);

        GameObject house = Instantiate(Houses[houseTier],
                       GetNewHousePos(left, right, houseTier),
                       Quaternion.Euler(0, -90, 0),
                       transform.parent);
        house.transform.localRotation = house.transform.rotation;
        //house.transform.localPosition += Vector3.back * houseTier * 0.1f;
        house.GetComponent<PropertyInfo>().PlayerID = playerID;
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
