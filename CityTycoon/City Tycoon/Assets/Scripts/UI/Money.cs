using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Money : MonoBehaviour
{
    private GameObject player;

    private void LateUpdate()
    {
        player = GameObject.Find("TurnManager").GetComponent<TurnManagment>().GetCurrentPlayer();

        gameObject.GetComponent<TextMeshProUGUI>().text = player.GetComponent<PlayerInfo>().getMoney().ToString() + "$";
    }
}
