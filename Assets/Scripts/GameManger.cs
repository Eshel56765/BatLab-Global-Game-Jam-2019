using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
        if(Instance==null)
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
        TimerController.Faild += HeFaildBigTime;

        RandomProblem = Random.Range(0, ProblemsTriggers.Count);
        CurrentProblemTrigger = Instantiate(ProblemsTriggers[RandomProblem].ProblemTrigger).gameObject;
        ProblemsTriggers[RandomProblem].ProblemParticleSystem.Play();
        FixedProblem = false;
    }

    private void HeFaildBigTime()
    {
        CashManager.AddMoney(-100);
        TimerController.gameObject.SetActive(false);
    }

    private IEnumerator Flow()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.F) && FadeInFadeOut.CanGoToWork && !FadeInFadeOut.IsFading && FixedProblem)
            {
                TimerController.StopTimer();
                CashManager.gameObject.SetActive(false);

                FixedProblemText.SetActive(false);
                StartCoroutine(FadeInFadeOut.FadeOut());
                yield return new WaitForSeconds(1f);
                MoneyFromWorkText.SetActive(true);
                yield return new WaitForSeconds(3f);
                MoneyFromWorkText.SetActive(false);
                yield return new WaitForSeconds(1f);

                
                TimerController.ResetTime();
                CashManager.gameObject.SetActive(true);
                StartCoroutine(RandomaizeGame());
                StartCoroutine(FadeInFadeOut.FadeIn());
            }
            

            yield return null;
        }
    }
    private IEnumerator RandomaizeGame()
    {
        foreach (Problems problem in ProblemsTriggers)
        {
            problem.ProblemParticleSystem.Stop();
            Destroy(CurrentProblemTrigger);
        }
        RandomProblem = Random.Range(0, ProblemsTriggers.Count);
        CurrentProblemTrigger = Instantiate(ProblemsTriggers[RandomProblem].ProblemTrigger).gameObject;
        ProblemsTriggers[RandomProblem].ProblemParticleSystem.Play();
        TimerController.ResetTime();
        StartCoroutine(TimerController.ClocksTicking());
        FixedProblem = false;
        yield return null;
    }

    public IEnumerator ProblemFixedTextShower()
    {
        FixedProblemText.SetActive(true);
        yield return new WaitForSeconds(3f);
        FixedProblemText.SetActive(false);
    }
}
