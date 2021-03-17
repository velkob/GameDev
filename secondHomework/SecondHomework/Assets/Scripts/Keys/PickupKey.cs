using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupKey : MonoBehaviour
{
    //Това мисля, че трябва да е метод, който да се
    //вика от класа, който действаше като медиатор между всички компоненти
    //Но поради липсата на такъв, съм направил логиката по извикването на метода тук
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Tim"))
        {
            Destroy(gameObject);
        }
    }
}
