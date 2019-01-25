using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CashManager : MonoBehaviour
{
    public Text Cash;
    public ParticleSystem MoneysExplosion;

    int maxAmountOfMoneys = 99999;

    public void AddMoney(int Amount)
    {
        int i = int.Parse(Cash.text);

        if (i + Amount > maxAmountOfMoneys)
            Cash.text = maxAmountOfMoneys.ToString();
        else
            Cash.text = (i + Amount).ToString().PadLeft(maxAmountOfMoneys.ToString().Length, '0');

        MoneysExplosion.Play();
    }
}
