using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToiletMinigameManager : MonoBehaviour
{

    public ToiletBowlController ToiletBowlController;
    public Text PumpsText;

    private int CatchCounter = 10;

    // Start is called before the first frame update
    void Start()
    {
        ToiletBowlController.Catch += CatchCallback;

        PumpsText.text = "Only " + "<color=red>" + CatchCounter + "</color>" + " More To Release The Blockage";
    }

    private void CatchCallback()
    {
        CatchCounter--;//"<color=yellow>"+normalWord[i]+"</color>"

        PumpsText.text = "Only " + "<color=red>" + CatchCounter + "</color>" + " More To Release The Blockage";

        if(CatchCounter == 0)
        {
            Win();
        }
    }

    private void Win()
    {
        throw new NotImplementedException();
    }
}
