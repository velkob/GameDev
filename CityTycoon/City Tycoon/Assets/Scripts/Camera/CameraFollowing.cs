using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowing : MonoBehaviour
{
    //    [SerializeField]
    //    private Transform player;
    //    [SerializeField]
    //    private Vector3 rotation;
    //    [SerializeField]
    //    private Vector3 offset;

    //    private void Start()
    //    {
    //        transform.eulerAngles = rotation;
    //    }
    //    void LateUpdate()
    //    {
    //        transform.position = player.position + offset;
    //        transform.eulerAngles = player.rotation.eulerAngles + rotation;
    //    }

    public float turnSpeed = 4.0f;
    public Transform player;

    private Vector3 offset;

    void Start()
    {
        offset = new Vector3(player.position.x, player.position.y + 45.0f, player.position.z + 7.0f);
        offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * offset;
        transform.position = player.position + offset;
        transform.LookAt(player.position);
    }

    void LateUpdate()
    {
        transform.position = player.position + offset;
        if (Input.GetMouseButton(1))
        {
            offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * offset;
            transform.LookAt(player.position);
        }
    }
}
