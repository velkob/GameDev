using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HearthLose : MonoBehaviour
{
    [SerializeField]
    private Sprite emptyHearth;

    [SerializeField]
    private int id;

    private SpriteRenderer spriteRenderer;

    private static int counter = 2;
    private void Start()
    {
        GameEvents.current.OnFallingToDeath += healthLose;
        spriteRenderer = transform.GetComponent<SpriteRenderer>();
    }

    void healthLose()
    {
        if (counter == id)
        {
            spriteRenderer.sprite = emptyHearth;
            counter--;
        }
    }
}
