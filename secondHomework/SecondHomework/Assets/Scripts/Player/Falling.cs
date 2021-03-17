using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling : MonoBehaviour
{
    private float deathBoarder = -3f;
    
    //Трябва да извиква метод за намаляне на животи, пак със този обект, който да действа като медиатор
    //Може би ще е по-добре, да има някакъв обект, като една линия отдолу, и да се използва collision detecting
    void Update()
    {
        if (transform.position.y <= deathBoarder)
        {
            transform.position = new Vector3(0, 0, 0);
        }
    }
}
