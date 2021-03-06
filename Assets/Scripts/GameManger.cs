﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManger : MonoBehaviour
{
    [System.Serializable]
    public struct Problems
    {
        public string ProblemName;
        public ParticleSystem ProblemParticleSystem;
        public ProblemTrigger ProblemTrigger;
    }

    public bool FixedProblem;
    public GameObject FixedProblemText;

    public Transform PlayerTarnsform;

    private float TimeToCloseText = 0;

    public int RandomProblem;
    public CashManager CashManager;
    public FadeInFadeOut FadeInFadeOut;
    public TimerController TimerController;
    public RectTransform UICanvas;

    public GameObject MoneyFromWorkText;

    public List<Problems> ProblemsTriggers = new List<Problems>();

    public int TheMoneysOfAwsome = 0;

    public GameObject CurrentProblemTrigger;

    public static GameManger Instance { get; private set; }
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("more than one GM");
            Destroy(this);
        }
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        StartCoroutine(Flow());
        CursorLockManager.ReleaseMouse(this);
        RenderSettings.fog = false;
        TimerController.Faild += () => { StartCoroutine(WhenHeFailsBigTime()); };

        RandomProblem = Random.Range(0, ProblemsTriggers.Count);
        CurrentProblemTrigger = Instantiate(ProblemsTriggers[RandomProblem].ProblemTrigger).gameObject;
        ProblemsTriggers[RandomProblem].ProblemParticleSystem.Play();
        FixedProblem = false;
    }

    private IEnumerator Flow()
    {
        while (true)
        {
            TimeToCloseText += Time.deltaTime;

            if(TimeToCloseText >= 6)
                FixedProblemText.SetActive(false);

            if (Input.GetKeyDown(KeyCode.F) && FadeInFadeOut.CanGoToWork && !FadeInFadeOut.IsFading && FixedProblem)
            {
                TimerController.StopTimer();
                CashManager.gameObject.SetActive(false);

                FixedProblemText.SetActive(false);
                StartCoroutine(FadeInFadeOut.FadeOut());
                MoneyFromWorkText.GetComponent<Text>().text = "You Have Gone And Did Them Worksess, You Got 100$ !!";
                yield return new WaitForSeconds(1f);
                MoneyFromWorkText.SetActive(true);
                yield return new WaitForSeconds(3f);
                MoneyFromWorkText.SetActive(false);
                yield return new WaitForSeconds(1f);


                TimerController.ResetTime();
                CashManager.gameObject.SetActive(true);
                StartCoroutine(RandomaizeGame());
                StartCoroutine(FadeInFadeOut.FadeIn());
                CashManager.AddMoney(100);
            }


            if (PlayerTarnsform.position.y < -10)
            {
                PlayerTarnsform.position = new Vector3(1.62f, 1.16f, 1.834f);
            }

            yield return null;
        }
    }
    private IEnumerator RandomaizeGame()
    {
        foreach (Problems problem in ProblemsTriggers)
        {
            problem.ProblemParticleSystem.Stop();
            CurrentProblemTrigger.SetActive(false);
            CurrentProblemTrigger.GetComponent<AudioSource>().Stop();
        }
        RandomProblem = Random.Range(0, ProblemsTriggers.Count);
        CurrentProblemTrigger = Instantiate(ProblemsTriggers[RandomProblem].ProblemTrigger).gameObject;
        ProblemsTriggers[RandomProblem].ProblemParticleSystem.Play();
        CurrentProblemTrigger.GetComponent<AudioSource>().Play();
        TimerController.ResetTime();
        StartCoroutine(TimerController.ClocksTicking());
        FixedProblem = false;
        yield return null;
    }

    public void ProblemFixedTextShower()
    {
        FixedProblemText.SetActive(true);
        TimeToCloseText = 0;
    }

    public IEnumerator WhenHeFailsBigTime()
    {
        CashManager.AddMoney(-100);

        TimerController.StopTimer();
        CashManager.gameObject.SetActive(false);
        FixedProblemText.SetActive(false);


        StartCoroutine(FadeInFadeOut.FadeOut());
        MoneyFromWorkText.GetComponent<Text>().text = "You Faild To Fix The Problem In Time To Get To Work ,\n You Have Been Charged 100$ !";
        yield return new WaitForSeconds(1f);
        MoneyFromWorkText.SetActive(true);
        yield return new WaitForSeconds(3f);
        MoneyFromWorkText.SetActive(false);
        yield return new WaitForSeconds(1f);


        TimerController.ResetTime();
        CashManager.gameObject.SetActive(true);
        StartCoroutine(RandomaizeGame());
        StartCoroutine(FadeInFadeOut.FadeIn());
        yield return null;
    }
}
