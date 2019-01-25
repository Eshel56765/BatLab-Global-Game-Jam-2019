using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProblemTrigger : MonoBehaviour
{
    public GameObject MiniGamePrefab;
    private Collider problemCollider;


    // Start is called before the first frame update
    void Start()
    {
        problemCollider = gameObject.GetComponent<Collider>();
    }

    private void OnTriggerStay(Collider Col)
    {
        if(Col.tag == "Player" && Input.GetKeyDown(KeyCode.F))
        {
            Instantiate(MiniGamePrefab);
        }
    }
}
