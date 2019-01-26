using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToiletMinigameManager : Minigame
{

    public ToiletBowlController ToiletBowlController;
    public Text PumpsText;

    private int CatchCounter = 10;

    // Start is called before the first frame update
    void Start()
    {
        ToiletBowlController.Catch += CatchCallback;

        PumpsText.text = "Only " + "<color=red>" + CatchCounter + "</color>" + " More To Release The Blockage";
        StartGame();
    }

    public override void StartGame()
    {
        CursorLockManager.UseMouse(this);
    }

    public override void EndGame()
    {
        CursorLockManager.ReleaseMouse(this);
        base.EndGame();
    }

    public override bool HasEnded => CatchCounter == 0;

    private void CatchCallback()
    {
        if (CatchCounter == 0) return;

        CatchCounter--;//"<color=yellow>"+normalWord[i]+"</color>"

        PumpsText.text = "Only " + "<color=red>" + CatchCounter + "</color>" + " More To Release The Blockage";

        if(HasEnded)
        {
            EndGame();
        }
    }
}
