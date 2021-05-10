using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowing : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    [SerializeField]
    private Vector3 rotation;
    [SerializeField]
    private Vector3 offset;

    private void Start()
    {
        transform.eulerAngles = rotation;
    }
    void LateUpdate()
    {
        transform.position = player.position + offset;
    }
}
