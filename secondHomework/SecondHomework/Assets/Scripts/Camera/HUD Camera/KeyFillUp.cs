using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyFillUp : MonoBehaviour
{
    [SerializeField]
    private Sprite filledKey;

    [SerializeField]
    private int id;

    private SpriteRenderer spriteRenderer;
    private void Start()
    {
        GameEvents.current.OnKeyPickUp += keyFill;
        spriteRenderer = transform.GetComponent<SpriteRenderer>();
    }

    void keyFill(int id)
    {
        if (this.id == id)
        {
            spriteRenderer.sprite = filledKey;
        }
    }
}
