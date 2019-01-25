using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMinigame : MonoBehaviour
{

    public Minigame Minigame;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            if (!Minigame.MinigameStarted)
                Minigame.StartGame();
    }
}
