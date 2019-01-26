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
        GameManger.Instance.ProblemsTriggers[GameManger.Instance.RandomProblem].ProblemParticleSystem.Stop();
        GameManger.Instance.TimerController.StopCoroutine(GameManger.Instance.TimerController.ClocksTicking());
        GameManger.Instance.FixedProblem = true;
        StartCoroutine(GameManger.Instance.ProblemFixedTextShower());
        GameManger.Instance.TimerController.StopTimer();

        GameManger.Instance.CurrentProblemTrigger.gameObject.SetActive(false);

        MinigameStarted = false;
        GameManger.Instance.CashManager.AddMoney((int)(GameManger.Instance.TimerController.TimePassed * 100));
        Destroy(gameObject);
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
