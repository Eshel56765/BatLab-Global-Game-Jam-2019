using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Minigame : MonoBehaviour
{
    public abstract void StartGame();

    public abstract void EndGame();
    
    public abstract bool HasEnded { get; }

    public bool MinigameStarted;
    public float TimeStarted;
}
