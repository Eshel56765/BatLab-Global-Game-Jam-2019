using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public Camera PlayerCamera;
    public float Speed;
    private Rigidbody rigidBody;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 velocity = new Vector3();
        Vector3 cameraForward = PlayerCamera.transform.forward;
        cameraForward.y = 0;
        cameraForward.Normalize();
        Vector3 cameraRight = PlayerCamera.transform.right;
        cameraRight.y = 0;
        cameraRight.Normalize();
        velocity = Input.GetAxis("Vertical") * cameraForward;
        velocity += Input.GetAxis("Horizontal") * cameraRight;
        if (velocity != Vector3.zero)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(velocity), 720 * Time.deltaTime);
            velocity.y = rigidBody.velocity.y / Speed;
            rigidBody.velocity = velocity * Speed;
        }
    }
}
