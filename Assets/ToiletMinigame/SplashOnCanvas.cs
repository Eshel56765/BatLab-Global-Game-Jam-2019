using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashOnCanvas : MonoBehaviour
{
    public GameObject SplashPrefab;

    public Vector3 Offset;

    //private void Update()
    //{
    //    transform.position = Input.mousePosition + Offset;
    //    if (Input.GetKeyDown(KeyCode.Mouse0))
    //    {
    //        StartCoroutine(Spray());
    //    }
    //    else if (Input.GetKeyUp(KeyCode.Mouse0))
    //    {
    //        StopAllCoroutines();
    //    }
    //}

    public void Splash(Transform SplashZone)
    {
        for (int i = 0; i < 11; i++)
        {
            RectTransform rt = SplashZone as RectTransform;
            Vector3 offset = new Vector3((0.5f - rt.pivot.x) * 134, 65, 0);
            Rigidbody2D smoke = Instantiate(SplashPrefab, SplashZone.position + offset, Quaternion.AngleAxis(Random.Range(0f, 360f), Vector3.forward), transform.parent).GetComponent<Rigidbody2D>();
            smoke.constraints = RigidbodyConstraints2D.FreezeRotation;
            smoke.velocity = new Vector2((i-5)*15, 150);
            Destroy(smoke.gameObject, 2);
        }
    }
}
