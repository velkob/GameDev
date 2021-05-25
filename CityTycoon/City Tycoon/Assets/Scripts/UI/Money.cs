using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Money : MonoBehaviour
{
    private int money;

    [SerializeField]
    private GameObject player;

    private void Update()
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = player.GetComponent<PlayerInfo>().getMoney().ToString() + "$";
    }
}
