using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Minigame : MonoBehaviour
{
    public GameObject UI;

    // Start is called before the first frame update
    void StartGame()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected abstract void EndGame();
}
