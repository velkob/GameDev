using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HittingWhiteBall : MonoBehaviour
{
    [SerializeField]
    private GameObject whiteBall;
  

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
