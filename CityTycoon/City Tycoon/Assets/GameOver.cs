using UnityEngine;
using System;

public class GameOver : MonoBehaviour
{
    public static GameOver current;

    private void Awake()
    {
        current = this;
    }

    public event Action<int> PlayerLoosesAction;
    public void PlayerLooses(int id)
    {
        PlayerLoosesAction?.Invoke(id);
    }

    public event Action EndGameAction;
    public void EndGame()
    {
        EndGameAction?.Invoke();
    }
}
