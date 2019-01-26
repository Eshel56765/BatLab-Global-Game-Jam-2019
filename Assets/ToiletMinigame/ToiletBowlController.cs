using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToiletBowlController : MonoBehaviour
{

    public float Speed;
    public GameObject SplashParticleSystem;
    public Transform SplashZone;
    public SplashOnCanvas SplashOnCanvas;
    private RectTransform RectTransform;

    public event Action Catch;

    // Start is called before the first frame update
    void Start()
    {
        RectTransform = GetComponent<RectTransform>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        mousePosition.y = 0;
        mousePosition.z = 0;

        mousePosition.x = Mathf.Lerp(0, 1, mousePosition.x);
        Debug.Log(mousePosition.x);

        RectTransform.pivot = mousePosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);

        Catch?.Invoke();

        SplashOnCanvas.Splash(transform);
    }
}
