using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    public Health health;
    public Keys keys;

    private void Awake()
    {
        current = this;
    }

    public event Action OnKeyPickUp;
    public void keyPickUp()
    {
        if (OnKeyPickUp != null)
        {
            OnKeyPickUp();
            
        }
    }

    public event Action OnJumpFromSpring;
    public void jumpFromSpring()
    {
        if (OnJumpFromSpring != null)
        {
            OnJumpFromSpring();
        }
    }

    public event Action OnDying;

    public void Dying()
    {
        if (OnDying != null)
        {
            OnDying();
        }
    }
}
