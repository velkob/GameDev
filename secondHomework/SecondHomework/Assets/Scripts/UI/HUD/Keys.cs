using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Keys : MonoBehaviour
{

    [SerializeField]
    private Image KeyImage;

    [SerializeField]
    private Sprite fullKeySprite;

    private int numberOfKeys;

    private int filledKeysCounter;

    void Start()
    {
        numberOfKeys = GameObject.FindGameObjectsWithTag("Key").Length;
        filledKeysCounter = 0;

        for (int i = 0; i < numberOfKeys - 1; i++)
        {
            Image currentKey = Instantiate(KeyImage);
            currentKey.transform.parent = GameObject.Find("Keys").transform;
            currentKey.transform.localScale = new Vector3(1, 1, 1);
        }
        GameEvents.current.OnKeyPickUp += fillKey;
    }

    public void fillKey()
    {
        Image key = transform.GetChild(filledKeysCounter++).GetComponent<Image>();
        key.sprite = fullKeySprite;
    }

    private void OnDestroy()
    {
        GameEvents.current.OnKeyPickUp -= fillKey;
    }
}
