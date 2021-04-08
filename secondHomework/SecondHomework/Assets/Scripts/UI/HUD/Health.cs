using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField]
    private Image HeartImage;

    [SerializeField]
    private Sprite emptyHeartSprite;

    [SerializeField]
    private int maxNumberOfHearts;

    private int heartsLeft;

    private void Start()
    {
        heartsLeft = maxNumberOfHearts - 1;

        for (int i = 0; i < maxNumberOfHearts - 1; i++)
        {
            Image currentHeart = Instantiate(HeartImage);
            currentHeart.transform.SetParent(GameObject.Find("Health").transform);
            currentHeart.transform.localScale = new Vector3(1, 1, 1);
        }

        GameEvents.current.OnDying += looseHealth;
    }
    private void looseHealth()
    {
            if (heartsLeft >= 0)
            {
                Image heart = transform.GetChild(heartsLeft--).GetComponent<Image>();
                heart.sprite = emptyHeartSprite;
            }
            else
            {
            
            }
    }

    private void OnDestroy()
    {
        GameEvents.current.OnDying -= looseHealth;
    }
}
