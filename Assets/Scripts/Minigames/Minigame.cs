using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Minigame : MonoBehaviour
{
    public abstract void StartGame();

    public virtual void EndGame()
    {
        OnGameEnd?.Invoke();
    }
    public event System.Action OnGameEnd;

    public abstract bool HasEnded { get; }

    public bool MinigameStarted;
    public float TimeStarted;
}
