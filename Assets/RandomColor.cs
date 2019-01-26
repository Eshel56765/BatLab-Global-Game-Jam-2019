using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomColor : MonoBehaviour
{
    public Graphic thang;

    private void Update()
    {
        if (null != thang)
            thang.color = Random.ColorHSV(0, 1, 0, 1, 0, 1, 1, 1);

    }
}
