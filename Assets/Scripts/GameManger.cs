using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManger : MonoBehaviour
{
    public CashManager CashManager;
    public FadeInFadeOut FadeInFadeOut;
    public TimerController TimerController;

    public List<GameObject> ProblemsTriggers = new List<GameObject>();

    public int TheMoneysOfAwsome = 0;

    public static GameManger instance=null;
    // Start is called before the first frame update
    void Awake()
    {
        if(instance==null)
        {
            instance = this;
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
        CursorLockManager.ReleaseMouse(this);
        RenderSettings.fog = false;
        TimerController.Faild += HeFaildBigTime;
        
    }

    private void HeFaildBigTime()
    {
        CashManager.AddMoney(-100);
        TimerController.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && FadeInFadeOut.CanGoToWork && !FadeInFadeOut.IsFading)
        {
            StartCoroutine(FadeInFadeOut.FadeOut());
        }
    }
}
