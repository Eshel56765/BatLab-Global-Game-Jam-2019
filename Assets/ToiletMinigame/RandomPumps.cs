using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomPumps : MonoBehaviour
{

    public GameObject PumpPrefab;
    public RectTransform InitRectTransform;

    public float TimeToRespawn;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitForNextPump());
    }

    private IEnumerator WaitForNextPump()
    {
        while(true)
        {
            yield return new WaitForSeconds(TimeToRespawn);

            SpawnPump();
        }
    }

    private void SpawnPump()
    {
        GameObject pump = Instantiate(PumpPrefab, InitRectTransform);

        int randomX = UnityEngine.Random.Range(-415, 515);
        Vector3 currentPos = pump.GetComponent<RectTransform>().position;
        currentPos.x = randomX;
        currentPos.y = 320f;
        pump.GetComponent<RectTransform>().localPosition = currentPos;

    }

    
}
