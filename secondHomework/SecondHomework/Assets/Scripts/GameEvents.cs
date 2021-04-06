using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    private void Awake()
    {
        current = this;
    }

    public event Action<int> OnKeyPickUp;
    public void keyPickUp(int id)
    {
        if (OnKeyPickUp != null)
        {
            OnKeyPickUp(id);
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
