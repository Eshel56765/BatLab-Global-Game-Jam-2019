using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhackAFireExtinguisher : MonoBehaviour
{
    public GameObject SmokePrefab;

    public Vector3 Offset;

    private void Update()
    {
        transform.position = Input.mousePosition + Offset;
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartCoroutine(Spray());
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            StopAllCoroutines();
        }
    }

    private IEnumerator Spray()
    {
        while (true)
        {
            Rigidbody2D smoke = Instantiate(SmokePrefab, transform.position - Offset, Quaternion.AngleAxis(Random.Range(0f, 360f), Vector3.forward), transform.parent).GetComponent<Rigidbody2D>();
            smoke.constraints = RigidbodyConstraints2D.FreezeRotation;
            smoke.velocity = new Vector2(-150, Random.Range(-37.5f, 37.5f));
            Destroy(smoke.gameObject, 17);
            yield return null;
        }
    }
}
