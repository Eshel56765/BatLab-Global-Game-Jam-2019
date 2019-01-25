using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManger : MonoBehaviour
{

    public FadeInFadeOut FadeInFadeOut;

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
        RenderSettings.fog = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && FadeInFadeOut.CanGoToWork && !FadeInFadeOut.IsFading)
        {
            StartCoroutine(FadeInFadeOut.FadeOut());
        }
    }
}
