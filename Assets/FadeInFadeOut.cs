using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FadeInFadeOut : MonoBehaviour
{
    public bool CanGoToWork;
    public bool IsFading = false;
    public Collider PlayerBodyCol;

    public CashManager cashManager;

    public Image BlackScreen;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(FadeOut());
        }
    }

    public void OnTriggerEnter(Collider collider)
    {
        if(collider.Equals(PlayerBodyCol))
        {
            CanGoToWork = true;
        }
    }

    public void OnTriggerExit(Collider collider)
    {
        if (collider.Equals(PlayerBodyCol))
        {
            CanGoToWork = false;
        }
    }

    IEnumerator FadeIn()
    {
        for (float i = 1; i >= 0; i -= Time.deltaTime)
        {
            BlackScreen.fillAmount = Mathf.Clamp01(i);
            yield return null;
        }
        BlackScreen.fillAmount = 0;
        IsFading = false;
    }

    IEnumerator FadeOut()
    {
        if (CanGoToWork && !IsFading)
        {
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                BlackScreen.fillAmount = Mathf.Clamp01(i);
                IsFading = true;
                yield return null;
            }
            BlackScreen.fillAmount = 1;
            StartCoroutine(FadeIn());
        }
    }

}
