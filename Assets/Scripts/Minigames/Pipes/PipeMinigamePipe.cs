using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;
public class PipeMinigamePipe : MonoBehaviour, IPointerClickHandler
{
    [Flags] public enum PipeNodes
    {
        Top = 1, Bottom  = 2, Left = 4, Right = 8
    }
    public enum FillType
    {
        None, TopDown, BottomUp, RTL, LTR
    }

    public float FillSpeed = 0.5f;

    public FillType Fill { get; private set; }

    public bool WaterisThrough(PipeNodes Node)
    {
        if (Node == PipeNodes.Top)
            return WaterFill.fillAmount > TopThreshold;
        if (Node == PipeNodes.Bottom)
            return WaterFill.fillAmount > BottomThreshold;
        if (Node == PipeNodes.Right)
            return WaterFill.fillAmount > RightThreshold;
        else 
            return WaterFill.fillAmount > LeftThreshold;
    }

    private float LeftThreshold = Mathf.Infinity, RightThreshold = Mathf.Infinity, TopThreshold = Mathf.Infinity, BottomThreshold = Mathf.Infinity;
    private Coroutine fillCoroutine;

    public RawImage Image;
    public Image WaterFill;
    public Mask Mask;
    private LayoutElement Element;

    private Texture str8, turn, three, four, str8M, turnM, threeM, fourM;

    public PipeNodes Nodes { get; private set; }
    private static PipeMinigamePipe selected;
    private static PipeMinigamePipe Selected
    {
        get
        {
            return selected;
        }
        set
        {
            selected = value;
            if (null != selected)
                value.EmptyPipesRecursively();
        }
    }

    private void EmptyPipesRecursively()
    {
        SetFill(FillType.None);
        fillCoroutine = null;
        while (sourcedPipes.Count > 0)
        {
            PipeMinigamePipe pipe = sourcedPipes[0];
            sourcedPipes.RemoveAt(0);
            pipe.EmptyPipesRecursively();
        }
        sourcedPipes.Clear();
    }
    public List<PipeMinigamePipe> sourcedPipes = new List<PipeMinigamePipe>();
    public void SetTextures(Texture Straight, Texture Turn, Texture Three, Texture Four, Texture StraightM, Texture TurnM, Texture ThreeM, Texture FourM)
    {
        str8 = Straight;
        turn = Turn;
        three = Three;
        four = Four;
        str8M = StraightM;
        turnM = TurnM;
        threeM = ThreeM;
        fourM = FourM;
    }

    public bool HasNode(PipeNodes Node)
    {
        switch (Node)
        {
            case PipeNodes.Bottom:
            {
                return (Nodes & PipeNodes.Bottom) == PipeNodes.Bottom;
            }
            case PipeNodes.Top:
            {
                return (Nodes & PipeNodes.Top) == PipeNodes.Top;
            }
            case PipeNodes.Left:
            {
                return (Nodes & PipeNodes.Left) == PipeNodes.Left;
            }
            default:
            {
                return (Nodes & PipeNodes.Right) == PipeNodes.Right;
            }
        }
    }

    public void SetTypes(PipeNodes NewNodes, bool Active)
    {
        SetFill(FillType.None);
        if (Active)
        {
            Nodes = Nodes | NewNodes;
        }
        else
        {
            Nodes = Nodes & ~NewNodes;
        }
        UpdateNodes();
    }

    private void SetTypesWithoutUpdate(PipeNodes NewNodes, bool Active)
    {
        SetFill(FillType.None);
        if (Active)
        {
            Nodes = Nodes | NewNodes;
        }
        else
        {
            Nodes = Nodes & ~NewNodes;
        }
    }

    private void UpdateNodes()
    {
        bool top    =   (Nodes  & PipeNodes.Top)    == PipeNodes.Top, 
             bottom =   (Nodes & PipeNodes.Bottom)  == PipeNodes.Bottom,
             left   =   (Nodes & PipeNodes.Left)    == PipeNodes.Left, 
             right  =   (Nodes & PipeNodes.Right)   == PipeNodes.Right;
        if (top)
        {
            if (bottom)
            {
                if (right)
                {
                    if (left)
                    {
                        Image.texture = four;
                        (Mask.graphic as RawImage).texture = fourM;
                    }
                    else
                    {
                        Image.texture = three;
                        (Mask.graphic as RawImage).texture = threeM;
                        Mask.transform.eulerAngles = Image.transform.eulerAngles = new Vector3();
                    }
                }
                else
                {
                    if (left)
                    {
                        Image.texture = three;
                        (Mask.graphic as RawImage).texture = threeM;
                        Mask.transform.eulerAngles = Image.transform.eulerAngles = new Vector3(0, 0, 180);
                    }
                    else
                    {
                        Image.texture = str8;
                        (Mask.graphic as RawImage).texture = str8M;
                        Mask.transform.eulerAngles = Image.transform.eulerAngles = new Vector3(0, 0, 0);
                    }
                }
            }
            else
            {
                if (right)
                {
                    if (left)
                    {
                        Image.texture = three;
                        (Mask.graphic as RawImage).texture = threeM;
                        Mask.transform.eulerAngles = Image.transform.eulerAngles = new Vector3(0, 0, 90);
                    }
                    else
                    {
                        Image.texture = turn;
                        (Mask.graphic as RawImage).texture = turnM;
                        Mask.transform.eulerAngles = Image.transform.eulerAngles = new Vector3(0, 0, 90);
                    }
                }
                else
                {
                    Image.texture = turn;
                    (Mask.graphic as RawImage).texture = turnM;
                    Mask.transform.eulerAngles = Image.transform.eulerAngles = new Vector3(0, 0, 180);
                }
            }
        }
        else if (bottom)
        {
            if (right)
            {
                if (left)
                {
                    Image.texture = three;
                    (Mask.graphic as RawImage).texture = threeM;
                    Mask.transform.eulerAngles = Image.transform.eulerAngles = new Vector3(0, 0, -90);
                }
                else
                {
                    Image.texture = turn;
                    (Mask.graphic as RawImage).texture = turnM;
                    Mask.transform.eulerAngles = Image.transform.eulerAngles = new Vector3();
                }
            }
            else
            {
                Image.texture = turn;
                (Mask.graphic as RawImage).texture = turnM;
                Mask.transform.eulerAngles = Image.transform.eulerAngles = new Vector3(0, 0, -90);
            }
        }
        else
        {
            Image.texture = str8;
            (Mask.graphic as RawImage).texture = str8M;
            Mask.transform.eulerAngles = Image.transform.eulerAngles = new Vector3(0, 0, 90);
        }
        WaterFill.transform.rotation = Quaternion.identity;
    }

    // Start is called before the first frame update
    void Start()
    {
        Element = GetComponent<LayoutElement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this == Selected)
        {
            transform.position = Input.mousePosition + new Vector3(-50, 50);
            if (Input.GetKeyDown(KeyCode.R))
            {
                Rotate();
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (null != Selected)
            {
                Selected.Element.enabled = true;
                Selected = null;
            }
        }
    }

    public void SetFill(FillType type)
    {
        if (this == Selected)
        {
            WaterFill.fillAmount = 0;
            return;
        }
        if (null != fillCoroutine)
            if (type == FillType.None)
            {
                StopCoroutine(fillCoroutine);
                fillCoroutine = null;
            }
            else
                return;
        Fill = type;
        bool top    =   (Nodes  & PipeNodes.Top)    == PipeNodes.Top, 
             bottom =   (Nodes & PipeNodes.Bottom)  == PipeNodes.Bottom,
             left   =   (Nodes & PipeNodes.Left)    == PipeNodes.Left, 
             right  =   (Nodes & PipeNodes.Right)   == PipeNodes.Right;
        switch (type)
        {
            case FillType.BottomUp:
                {
                    WaterFill.fillMethod = UnityEngine.UI.Image.FillMethod.Vertical;
                    WaterFill.fillOrigin = (int) UnityEngine.UI.Image.OriginVertical.Bottom;
                    BottomThreshold = 0;
                    TopThreshold = top ? 0.99f : Mathf.Infinity;
                    RightThreshold = right ? 0.6f : Mathf.Infinity;
                    LeftThreshold = left? 0.6f : Mathf.Infinity;
                    WaterFill.fillAmount = 0;
                    if ((Nodes & PipeNodes.Bottom) == PipeNodes.Bottom)
                        fillCoroutine = StartCoroutine(StartFill());
                    break;
                }
            case FillType.TopDown:
                {
                    WaterFill.fillMethod = UnityEngine.UI.Image.FillMethod.Vertical;
                    WaterFill.fillOrigin = (int)UnityEngine.UI.Image.OriginVertical.Top;
                    TopThreshold = 0;
                    BottomThreshold = bottom ? 0.99f : Mathf.Infinity;
                    RightThreshold = right ? 0.6f : Mathf.Infinity;
                    LeftThreshold = left ? 0.6f : Mathf.Infinity;
                    WaterFill.fillAmount = 0;
                    if ((Nodes & PipeNodes.Top) == PipeNodes.Top)
                        fillCoroutine = StartCoroutine(StartFill());
                    break;
                }
            case FillType.LTR:
                {
                    WaterFill.fillMethod = UnityEngine.UI.Image.FillMethod.Horizontal;
                    WaterFill.fillOrigin = (int)UnityEngine.UI.Image.OriginHorizontal.Left;
                    TopThreshold = top ? 0.6f : Mathf.Infinity;
                    BottomThreshold = bottom ? 0.6f : Mathf.Infinity;
                    RightThreshold = right ? 0.99f : Mathf.Infinity;
                    LeftThreshold = 0;
                    WaterFill.fillAmount = 0;
                    if ((Nodes & PipeNodes.Left) == PipeNodes.Left)
                        fillCoroutine = StartCoroutine(StartFill());
                    break;
                }
            case FillType.RTL:
                {
                    WaterFill.fillMethod = UnityEngine.UI.Image.FillMethod.Horizontal;
                    WaterFill.fillOrigin = (int)UnityEngine.UI.Image.OriginHorizontal.Right;
                    TopThreshold = top ? 0.6f : Mathf.Infinity;
                    BottomThreshold = bottom ? 0.6f : Mathf.Infinity;
                    RightThreshold = 0;
                    LeftThreshold = left ? 0.99f : Mathf.Infinity;
                    WaterFill.fillAmount = 0;
                    if ((Nodes & PipeNodes.Right) == PipeNodes.Right)
                        fillCoroutine = StartCoroutine(StartFill());
                    break;
                }
            case FillType.None:
                {
                    if (null != fillCoroutine)
                    {
                        StopCoroutine(fillCoroutine);
                        fillCoroutine = null;
                    }
                    RightThreshold = LeftThreshold = TopThreshold = BottomThreshold = Mathf.Infinity;
                    WaterFill.fillAmount = 0;
                    return;
                }
        }
    }

    public void Rotate()
    {
        bool hadLeft = HasNode(PipeNodes.Left);
        SetTypesWithoutUpdate(PipeNodes.Left, HasNode(PipeNodes.Bottom));
        SetTypesWithoutUpdate(PipeNodes.Bottom, HasNode(PipeNodes.Right));
        SetTypesWithoutUpdate(PipeNodes.Right, HasNode(PipeNodes.Top));
        SetTypes(PipeNodes.Top, hadLeft);
    }

    private IEnumerator StartFill()
    {
        float startTime = Time.time;
        do
        {
            WaterFill.fillAmount = (Time.time - startTime) * FillSpeed;
            yield return null;
        } while (WaterFill.fillAmount < 0.98f);
        WaterFill.fillAmount = 1;
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData data)
    {
        PipeNodes selectedNodes = (PipeNodes)(-1);
        if (null != Selected)
        {
            Selected.Element.enabled = true;
            selectedNodes = Selected.Nodes;
            Selected.SetTypes(selectedNodes, false);
            Selected.SetTypes(Nodes, true);
            Selected.SetFill(FillType.None);
            SetTypes(Nodes, false);
            SetTypes(selectedNodes, true);
            Selected = null;
        }
        else
        {
            Selected = this;
            Element.enabled = false;
            SetFill(FillType.None);
        }
    }
}
