using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpAndThrow : MonoBehaviour
{
    public GameObject Shoulder;
    private GameObject HeldObject = null;
    private Animator anime;
    // Start is called before the first frame update
    void Start()
    {
        anime = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (HeldObject == null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (other.gameObject.CompareTag("Pickable"))
                {
                    HeldObject = other.gameObject;
                    HeldObject.transform.parent = Shoulder.transform;
                    HeldObject.transform.localPosition = new Vector3(0,0,1.85f);
                    anime.SetBool("Pick", true);
                }
            }
        }
    }
}
