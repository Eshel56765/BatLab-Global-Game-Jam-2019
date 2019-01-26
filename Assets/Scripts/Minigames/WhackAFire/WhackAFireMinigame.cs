using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WhackAFireMinigame : Minigame
{
    public Transform FiredGrid;
    public GameObject FirePrefab;
    public Texture BigFire, MediumFire, SmallFire;

    public WhackAFireFire[,] Fires = new WhackAFireFire[3, 3];
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(2);
        StartGame();
    }

    public override void StartGame()
    {
        CursorLockManager.UseMouse(this);
        TimeStarted = Time.time;
        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                Fires[x, y] = Instantiate(FirePrefab, FiredGrid).GetComponent<WhackAFireFire>();
                Fires[x, y].SetReferences(BigFire, MediumFire, SmallFire, this);
            }
        }
    }

    public static int ExtingiushedFires { get; private set; }

    public static void FireExtinguished()
    {
        ExtingiushedFires++;
    }

    public override void EndGame()
    {
        base.EndGame();
        Destroy(gameObject, 2);
    }

    public override bool HasEnded
    {
        get
        {
            return ExtingiushedFires > 40 && AllFiresExtinguished();
        }
    }

    private bool AllFiresExtinguished()
    {
        for (int y = 0; y < 3; y++)
        {
            for (int x = 0; x < 3; x++)
            {
                if (Fires[x, y].IsOnFire)
                    return false;
            }
        }
        return true;
    }
    // Update is called once per frame
    void Update()
    {
        if (HasEnded)
            EndGame();
    }
}
