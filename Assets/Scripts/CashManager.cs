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

    private void Update()
    {
        if (GameManger.Instance.TheMoneysOfAwsome < 0)
        {
            ChangeColorRed();
        }
        else
        {
            ResetColor();
        }
    }

    public void AddMoney(int Amount)
    {
        if (GameManger.Instance.TheMoneysOfAwsome + Amount >= maxAmountOfMoneys)
            return;

        Cash.text = (GameManger.Instance.TheMoneysOfAwsome + Amount).ToString("D5");
        GameManger.Instance.TheMoneysOfAwsome += Amount;
       
        MoneysExplosion.Play();
    }

    public void ChangeColorRed()
    {
        Cash.color = Color.red;
        CashImage.color = Color.red;
    }
    public void ResetColor()
    {
        Cash.color = startingColorText;
        CashImage.color = startingColorText;
    }
}
