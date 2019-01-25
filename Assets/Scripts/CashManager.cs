using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CashManager : MonoBehaviour
{
    public Image CashImage;
    public Text Cash;
    public ParticleSystem MoneysExplosion;

    private Color startingColorText, StartingColorCashImage;

    int maxAmountOfMoneys = 99999;

    private void Start()
    {
        startingColorText = Cash.color;
        StartingColorCashImage = CashImage.color;
    }

    public void AddMoney(int Amount)
    {
        if (GameManger.instance.TheMoneysOfAwsome + Amount >= maxAmountOfMoneys)
            return;
         
        Cash.text = (GameManger.instance.TheMoneysOfAwsome + Amount).ToString("D5");
        GameManger.instance.TheMoneysOfAwsome += Amount;
        if (GameManger.instance.TheMoneysOfAwsome < 0)
        {
            Cash.color = Color.red;
            CashImage.color = Color.red;
        }
        else
        {
            Cash.color = startingColorText;
            CashImage.color = startingColorText;
        }
        MoneysExplosion.Play();
    }
}
