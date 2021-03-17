using UnityEngine;

public class CameraFollowing : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 0.256f;
    public Vector3 offset;

    void LateUpdate()
    {
        transform.position = player.position + offset;
    }
}
