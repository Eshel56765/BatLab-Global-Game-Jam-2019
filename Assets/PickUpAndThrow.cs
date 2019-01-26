using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpAndThrow : MonoBehaviour
{
    public GameObject Shoulder;
    public float Force;
    private GameObject HeldObject = null;
    
    private Animator anime;
    private bool firstUpdate = false;
    // Start is called before the first frame update
    void Start()
    {
        anime = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (HeldObject != null)
        {
            if (!firstUpdate)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    HeldObject.transform.parent = null;
                    HeldObject.GetComponent<Rigidbody>().isKinematic = false;
                    HeldObject.GetComponent<Rigidbody>().mass = 4;
                    if (!HeldObject.name.Contains("Couch")&&!HeldObject.name.Contains("Flowers"))
                    {
                        HeldObject.GetComponent<BoxCollider>().enabled = true;
                    }
                    HeldObject.GetComponent<Rigidbody>().AddForce(transform.forward * Force);
                    HeldObject = null;
                    anime.SetBool("Pick", false);
                    
                }
            }
            else
            {
                firstUpdate = false;
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (HeldObject == null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if(other.gameObject.name.Contains("Cube."))
                {
                    HeldObject = other.gameObject.transform.parent.gameObject;
                    HeldObject.transform.parent = Shoulder.transform;
                    HeldObject.transform.localPosition = new Vector3(0, 0,3f);
                    HeldObject.GetComponent<Rigidbody>().isKinematic = true;
                    HeldObject.GetComponent<Rigidbody>().mass = 0;
                   // HeldObject.GetComponent<BoxCollider>().enabled = false;
                    anime.SetBool("Pick", true);
                    firstUpdate = true;
                }
                if (other.gameObject.name.Contains("Vase"))
                {
                    HeldObject = other.gameObject.transform.parent.gameObject;
                    HeldObject.transform.parent = Shoulder.transform;
                    HeldObject.transform.localPosition = new Vector3(0, 0, 2f);
                    HeldObject.GetComponent<Rigidbody>().isKinematic = true;
                    HeldObject.GetComponent<Rigidbody>().mass = 0;
                    // HeldObject.GetComponent<BoxCollider>().enabled = false;
                    anime.SetBool("Pick", true);
                    firstUpdate = true;
                }
                if (other.gameObject.CompareTag("Pickable"))
                {
                    HeldObject = other.gameObject;
                    HeldObject.transform.parent = Shoulder.transform;
                    HeldObject.transform.localPosition = new Vector3(0, 0, 1.85f);
                    HeldObject.GetComponent<Rigidbody>().isKinematic = true;
                    HeldObject.GetComponent<Rigidbody>().mass = 0;
                    HeldObject.GetComponent<BoxCollider>().enabled = false;
                    anime.SetBool("Pick", true);
                    firstUpdate = true;
                }
                if(other.gameObject.name.Contains("Door"))
                {
                    GameObject door = other.transform.parent.gameObject;
                    if(door.transform.eulerAngles.y==90)
                    {
                        door.transform.eulerAngles = new Vector3(door.transform.eulerAngles.x, 180, door.transform.eulerAngles.z);
                    }
                    else
                    {
                        door.transform.eulerAngles = new Vector3(door.transform.eulerAngles.x, 90, door.transform.eulerAngles.z);
                    }
                }

            }
        }
    }
}