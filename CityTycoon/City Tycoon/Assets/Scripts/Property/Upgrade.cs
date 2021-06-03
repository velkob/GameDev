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
            int number = GetHouseTier(left) + GetHouseTier(right);
            if (number + 1 <= Houses.Length)
            {
                return true;
            }
        }
        else if (right != null)
        {
            int number = GetHouseTier(right);
            if (number + 1 <= Houses.Length)
            {
                return true;
            }
        }
        else if (left != null)
        {
            int number = GetHouseTier(left);
            if (number + 1 <= Houses.Length)
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
            int number = 0;
            GameObject left, right;
            left = CheckLeftNeighbour();
            right = CheckRightNeighbour();
            if (left && right != null)
            {
                number = GetHouseTier(left) + GetHouseTier(right);
            }
            else if (right != null)
            {
                number = GetHouseTier(right);

            }
            else if (left != null)
            {
                number = GetHouseTier(left);
            }
            return prices[number];
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
            int number;
            if (left && right != null)
            {
                number = GetHouseTier(left) + GetHouseTier(right);
                left.SetActive(false);
                right.SetActive(false);
                gameObject.SetActive(false);
                GameObject house = Instantiate(Houses[number],
                    GetNewHousePos(left, right),
                    Quaternion.Euler(0, -90, 0),
                    transform.parent);
                house.transform.localRotation = house.transform.rotation;
                house.GetComponent<PropertyInfo>().Rent = rents[number];
            }
            else if (right != null)
            {
                number = GetHouseTier(right);
                right.SetActive(false);
                gameObject.SetActive(false);
                GameObject house = Instantiate(Houses[number],
                    GetNewHousePos(right),
                    Quaternion.Euler(0, -90, 0),
                    transform.parent);
                house.transform.localRotation = house.transform.rotation;
                house.GetComponent<PropertyInfo>().Rent=rents[number];
            }
            else if (left != null)
            {
                number = GetHouseTier(left);
                left.SetActive(false);
                gameObject.SetActive(false);
                GameObject house = Instantiate(Houses[number],
                    GetNewHousePos(left),
                    Quaternion.Euler(0, -90, 0),
                    transform.parent);
                house.transform.localRotation = house.transform.rotation;
                house.GetComponent<PropertyInfo>().Rent=rents[number];
            }
        }
    }

    private int GetHouseTier(GameObject house)
    {
        return int.Parse(house.tag[1].ToString());
    }

    private Vector3 GetNewHousePos(GameObject house)
    {
        return Vector3.Lerp(new Vector3(transform.position.x, 0, transform.position.z),
            new Vector3(house.transform.position.x, 0, house.transform.position.z),
            0.5f);
    }

    private Vector3 GetNewHousePos(GameObject house1, GameObject house2)
    {
        Vector3 firstLerp = Vector3.Lerp(new Vector3(transform.position.x, 0, transform.position.z),
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
