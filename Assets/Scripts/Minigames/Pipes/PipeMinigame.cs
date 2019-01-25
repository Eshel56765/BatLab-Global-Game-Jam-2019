using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PipeMinigame : Minigame
{
    public int Width, Height;
    private int Amount;
    public GameObject PipeImagePrefab, StartPrefab, EndPrefab, EmptyPrefab;
    public Texture Straight, Turn, Three, Four, StraightM, TurnM, ThreeM, FourM;

    private PipeMinigamePipe[,] Pipes;

    public int StraightPipesAmount, TurnPipesAmount, ThreePipesAmount, FourPipesAmount;

    [System.Serializable] public struct Point { public int x, y; }
    private float timeSinceLastUpdate;

    public Point StartPoint, EndPoint;

    private void Start()
    {
        StartGame();
    }

    // Start is called before the first frame update
    public override void StartGame()
    {
        GridLayoutGroup layout = GetComponent<GridLayoutGroup>();
        Vector2 size = layout.cellSize;//(PipeImagePrefab.transform as RectTransform).sizeDelta;
        size.x = size.x * (Width + 1) + 20;
        size.y = size.y * (Height + 1) + 20;
        RectTransform rTransform = transform as RectTransform;
        rTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size.x);
        rTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size.y);
        int usedStraight = 0, usedTurn = 0, usedThree = 0, usedFour = 0;
        Pipes = new PipeMinigamePipe[Width, Height];
        for (int y = 0; y < Height + 1; y++)
        {
            for (int x = 0; x < Width + 1; x++)
            {
                if (x < Width && y < Height)
                {
                    Pipes[x, y] = Instantiate(PipeImagePrefab, transform).GetComponent<PipeMinigamePipe>();
                    Pipes[x, y].SetTextures(Straight, Turn, Three, Four, StraightM, TurnM, ThreeM, FourM);
                    int rand = Random.Range(0, Amount - usedStraight - usedTurn - usedThree - usedFour);
                    if (rand < StraightPipesAmount - usedStraight)
                    {
                        rand = Random.Range(0, 2);
                        if (rand == 0)
                            Pipes[x, y].SetTypes(PipeMinigamePipe.PipeNodes.Bottom | PipeMinigamePipe.PipeNodes.Top, true);
                        else
                            Pipes[x, y].SetTypes(PipeMinigamePipe.PipeNodes.Right | PipeMinigamePipe.PipeNodes.Left, true);
                        usedStraight++;
                    }
                    else if (rand < StraightPipesAmount - usedStraight + TurnPipesAmount - usedTurn)
                    {
                        rand = Random.Range(0, 4);
                        if (rand == 0)
                            Pipes[x, y].SetTypes(PipeMinigamePipe.PipeNodes.Bottom | PipeMinigamePipe.PipeNodes.Right, true);
                        else if (rand == 1)
                            Pipes[x, y].SetTypes(PipeMinigamePipe.PipeNodes.Right | PipeMinigamePipe.PipeNodes.Top, true);
                        else if (rand == 2)
                            Pipes[x, y].SetTypes(PipeMinigamePipe.PipeNodes.Top | PipeMinigamePipe.PipeNodes.Left, true);
                        else
                            Pipes[x, y].SetTypes(PipeMinigamePipe.PipeNodes.Left | PipeMinigamePipe.PipeNodes.Bottom, true);
                        usedTurn++;
                    }
                    else if (rand < StraightPipesAmount - usedStraight + TurnPipesAmount - usedTurn + ThreePipesAmount - usedThree)
                    {
                        rand = Random.Range(0, 4);
                        if (rand == 0)
                            Pipes[x, y].SetTypes(PipeMinigamePipe.PipeNodes.Bottom | PipeMinigamePipe.PipeNodes.Right | PipeMinigamePipe.PipeNodes.Top, true);
                        else if (rand == 1)
                            Pipes[x, y].SetTypes(PipeMinigamePipe.PipeNodes.Right | PipeMinigamePipe.PipeNodes.Top | PipeMinigamePipe.PipeNodes.Left, true);
                        else if (rand == 2)
                            Pipes[x, y].SetTypes(PipeMinigamePipe.PipeNodes.Top | PipeMinigamePipe.PipeNodes.Left | PipeMinigamePipe.PipeNodes.Bottom, true);
                        else
                            Pipes[x, y].SetTypes(PipeMinigamePipe.PipeNodes.Left | PipeMinigamePipe.PipeNodes.Bottom | PipeMinigamePipe.PipeNodes.Right, true);
                        usedThree++;
                    }
                    else
                    {
                        Pipes[x, y].SetTypes(PipeMinigamePipe.PipeNodes.Bottom | PipeMinigamePipe.PipeNodes.Left | PipeMinigamePipe.PipeNodes.Right | PipeMinigamePipe.PipeNodes.Top, true);
                        usedFour++;
                    }
                }
                else
                {
                    if (x == EndPoint.x && y == EndPoint.y)
                    {
                        Instantiate(EndPrefab, transform);
                    }
                    else if (x == StartPoint.x && y == StartPoint.y)
                    {
                        Instantiate(StartPrefab, transform);
                    }
                    else
                    {
                        Instantiate(EmptyPrefab, transform);
                    }
                }
            }
        }
        System.Text.StringBuilder b = new System.Text.StringBuilder("Straight: ").Append(usedStraight).Append(", Turn: ").Append(usedTurn).Append(", Three:").Append(usedThree).Append(", Four: "); b.Append(usedFour);
        Debug.Log(b.ToString());
        MinigameStarted = true;
        TimeStarted = Time.time;
    }

    private void Update()
    {
        if (!MinigameStarted || Time.time - TimeStarted < 3)
            return;
        if (Time.time - timeSinceLastUpdate > 0.5f)
        {
            timeSinceLastUpdate = Time.time;
            Pipes[StartPoint.x, StartPoint.y - 1].SetFill(PipeMinigamePipe.FillType.BottomUp);
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (Pipes[x, y].WaterisThrough(PipeMinigamePipe.PipeNodes.Left))
                    {
                        if (0 < x && Pipes[x -1, y].HasNode(PipeMinigamePipe.PipeNodes.Right) && Pipes[x, y].HasNode(PipeMinigamePipe.PipeNodes.Left))
                        {
                            Pipes[x - 1, y].SetFill(PipeMinigamePipe.FillType.RTL);
                            Pipes[x, y].sourcedPipes.Add(Pipes[x - 1, y]);
                        }
                    }
                    if (Pipes[x, y].WaterisThrough(PipeMinigamePipe.PipeNodes.Right))
                    {
                        if (x < Width - 1 && Pipes[x + 1, y].HasNode(PipeMinigamePipe.PipeNodes.Left) && Pipes[x, y].HasNode(PipeMinigamePipe.PipeNodes.Right))
                        {
                            Pipes[x + 1, y].SetFill(PipeMinigamePipe.FillType.LTR);
                            Pipes[x, y].sourcedPipes.Add(Pipes[x + 1, y]);
                        }
                    }
                    if (Pipes[x, y].WaterisThrough(PipeMinigamePipe.PipeNodes.Top))
                    {
                        if (0 < y && Pipes[x, y - 1].HasNode(PipeMinigamePipe.PipeNodes.Bottom) && Pipes[x, y].HasNode(PipeMinigamePipe.PipeNodes.Top))
                        {
                            Pipes[x, y - 1].SetFill(PipeMinigamePipe.FillType.BottomUp);
                            Pipes[x, y].sourcedPipes.Add(Pipes[x, y - 1]);
                        }
                    }
                    if (Pipes[x, y].WaterisThrough(PipeMinigamePipe.PipeNodes.Bottom))
                    {
                        if (y < Height - 1 && Pipes[x, y + 1].HasNode(PipeMinigamePipe.PipeNodes.Top) && Pipes[x, y].HasNode(PipeMinigamePipe.PipeNodes.Bottom))
                        {
                            Pipes[x, y + 1].SetFill(PipeMinigamePipe.FillType.RTL);
                            Pipes[x, y].sourcedPipes.Add(Pipes[x, y + 1]);
                        }
                    }
                }
                if (Pipes[EndPoint.x - 1, EndPoint.y].HasNode(PipeMinigamePipe.PipeNodes.Right) && Pipes[EndPoint.x - 1, EndPoint.y].WaterisThrough(PipeMinigamePipe.PipeNodes.Right))
                {
                    EndGame();
                }
            }
        }
    }

    void OnValidate()
    {
        Amount = Width * Height;
        StraightPipesAmount = Mathf.Clamp(StraightPipesAmount, 0, Amount);
        TurnPipesAmount = Mathf.Clamp(TurnPipesAmount, 0, Amount - StraightPipesAmount);
        ThreePipesAmount = Mathf.Clamp(ThreePipesAmount, 0, Amount - StraightPipesAmount - TurnPipesAmount);

        FourPipesAmount = Amount - StraightPipesAmount - TurnPipesAmount - ThreePipesAmount;
        EndPoint.x = Width;
        EndPoint.y = Mathf.Clamp(EndPoint.y, 0, Height);
        StartPoint.x = Mathf.Clamp(StartPoint.x, 0, Width);
        StartPoint.y = Height;
    }

    public override void EndGame()
    {
        Destroy(transform.parent.gameObject);
        base.EndGame();
    }

    override public bool HasEnded
    {
        get
        {
            return Pipes[EndPoint.x - 1, EndPoint.y].WaterisThrough(PipeMinigamePipe.PipeNodes.Right);
        }
    }

}
