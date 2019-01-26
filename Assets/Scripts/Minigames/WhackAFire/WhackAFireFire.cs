using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WhackAFireFire : MonoBehaviour
{
    public bool IsOnFire { get; private set; }

    public RawImage FireImage;

    private WhackAFireMinigame minigame;

    private Color Transparent = new Color(0, 0, 0, 0);
    private Texture Big, Medium, Small;
    private Collider2D Col;
    private float TimeUntilNextFire;
    private int SmokesAbsorbed;

    enum FireSize
    {
        None, Small, Medium, Big
    }
    private FireSize Size;

    private void Start()
    {
        FireImage.color = Transparent;
        Col = GetComponent<Collider2D>();
        Col.enabled = false;
        TimeUntilNextFire = Random.Range(1f, 7f);
    }

    public void SetReferences(Texture Big, Texture Medium, Texture Small, WhackAFireMinigame Minigame)
    {
        this.Big = Big;
        this.Medium = Medium;
        this.Small = Small;
        minigame = Minigame;
    }

    private void SetFire(FireSize size)
    {
        switch (size)
        {
            case FireSize.Big:
            {
                FireImage.texture = Big;
                IsOnFire = true;
                break;
            }
            case FireSize.Medium:
            {
                FireImage.texture = Medium;
                IsOnFire = true;
                break;
            }
            case FireSize.Small:
            {
                FireImage.texture = Small;
                IsOnFire = true;
                break;
            }
            case FireSize.None:
            {
                FireImage.texture = null;
                IsOnFire = false;
                WhackAFireMinigame.FireExtinguished();
                break;
            }
        }
        Col.enabled = IsOnFire;
        FireImage.color = IsOnFire ? Color.white : Transparent;
        Size = size;
    }

    // Update is called once per frame
    void Update()
    {
        FireImage.transform.rotation = Quaternion.AngleAxis(Random.Range(-7.5f, 7.5f), Vector3.forward);
        TimeUntilNextFire -= Time.deltaTime;
        if (TimeUntilNextFire < 0)
        {
            if (WhackAFireMinigame.ExtingiushedFires < 40)
                TimeUntilNextFire = Random.Range(4f, 7f);
            else
                TimeUntilNextFire = Random.Range(8f, 10f); 
            if (!minigame.HasEnded)
                StartFire();
        }
    }

    private void StartFire()
    {
        SetFire(FireSize.Big);
        FireImage.color = Color.white;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SmokesAbsorbed++;
        Destroy(collision.gameObject, 0.1f);
        if (IsOnFire && SmokesAbsorbed > 12)
        {
            SmokesAbsorbed = 0;
            SetFire(Size - 1);
        }
    }
}
