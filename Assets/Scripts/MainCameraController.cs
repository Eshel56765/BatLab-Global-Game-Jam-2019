using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    public float TurnSpeed = 3f;
    public Transform Player;
    public float yOffset=6f;
    public float zOffset = 3f;

    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = new Vector3(Player.position.x, Player.position.y + yOffset, Player.position.z +zOffset);

    }

    // Update is called once per frame
    void Update()
    {
        offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * TurnSpeed, Vector3.up)*offset;
        transform.position = Player.position + offset;
        transform.LookAt(Player.position);
    }
}
