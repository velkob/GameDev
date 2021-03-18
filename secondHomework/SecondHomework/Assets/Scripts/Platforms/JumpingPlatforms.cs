using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingPlatforms : MonoBehaviour
{
  
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(1);
        if (collision.collider.name.Equals("Tim"))
        {
            GameEvents.current.jumpFromSpring();
        }
    }

    
}
