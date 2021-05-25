using System.Text.RegularExpressions;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    [SerializeField]
    GameObject[] Houses;

    [SerializeField]
    int[] prices;
    private void Start()
    {
       // BusinessEvents.current.OnUpgradingProperty += UpgradeBuilding;
    }

    public bool CheckIfUpgradable()
    {
        GameObject left, right;
        left = CheckLeftNeighbour();
        right = CheckRightNeighbour();

        if (left && right != null)
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

    public int UpgradeBuilding()
    {
        GameObject go = GameObject.Find("Player").GetComponent<LocateObject>().GetObject();

        if (go.GetComponent<PropertyInfo>().getId() == gameObject.GetComponent<PropertyInfo>().getId())
        {
            int number = 0;
            GameObject left, right;
            left = CheckLeftNeighbour();
            right = CheckRightNeighbour();
            if (left && right != null)
            {
                number = GetHouseTier(left) + GetHouseTier(right);
                left.SetActive(false);
                right.SetActive(false);
                gameObject.SetActive(false);
                Instantiate(Houses[number + 1],
                    GetNewHousePos(left, right),
                    Quaternion.Euler(0, -90, 0),
                    transform.parent);
            }
            else if (right != null)
            {
                number = GetHouseTier(right);
                right.SetActive(false);
                gameObject.SetActive(false);
                Instantiate(Houses[number + 1],
                    GetNewHousePos(right),
                    Quaternion.Euler(0, -90, 0),
                    transform.parent);

            }
            else if (left != null)
            {
                number = GetHouseTier(left);
                left.SetActive(false);
                gameObject.SetActive(false);
                Instantiate(Houses[number],
                    GetNewHousePos(left),
                    Quaternion.Euler(0, -90, 0),
                    transform.parent);

            }
            return prices[number];
        }
        return -1;
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
        //bool hit;
        hit = Physics.BoxCast(transform.position + Vector3.up, transform.localScale * 3, Vector3.left, out raycastHit, transform.rotation, 30);
        string raycastHitTag = hit != false ? Regex.Match(raycastHit.collider.tag, "T\\dBuilding").Value : "DefinetlyNotATagName";
        raycastHitTag = string.IsNullOrEmpty(raycastHitTag) ? "DefinetlyNotATagName" : raycastHitTag;

        Debug.Log(hit != false ? raycastHit.collider.tag : "");

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
        hit = Physics.BoxCast(transform.position, transform.localScale * 3, Vector3.right, out RaycastHit raycastHit, transform.rotation, 30);
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

    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    GameObject go = GameObject.Find("Player").GetComponent<LocateObject>().GetObject();

    //    //Check if there has been a hit yet
    //    if (hit)
    //    {
    //        //Draw a Ray forward from GameObject toward the hit
    //        Gizmos.DrawRay(transform.position + Vector3.up, Vector3.left * raycastHit.distance);
    //        //Draw a cube that extends to where the hit exists
    //        Gizmos.DrawWireCube(transform.position + Vector3.up + Vector3.left * raycastHit.distance, transform.localScale * 3);
    //    }
    //    //If there hasn't been a hit yet, draw the ray at the maximum distance
    //    else if (go.GetComponent<PropertyInfo>().getId() == gameObject.GetComponent<PropertyInfo>().getId())
    //    {
    //        //Draw a Ray forward from GameObject toward the maximum distance
    //        Gizmos.DrawRay(transform.position + Vector3.up, Vector3.left * 30);
    //        //Draw a cube at the maximum distance
    //        Gizmos.DrawWireCube(transform.position + Vector3.up + Vector3.left * 30, transform.localScale * 3);

    //    }

    //}
}
