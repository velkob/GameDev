using UnityEngine;
//public class CameraFollowing : MonoBehaviour
//{
//    public float turnSpeed = 4.0f;
//    public Transform player;

//    private Vector3 offset;

//    void Start()
//    {
//        offset = new Vector3(player.position.x, player.position.y + 45.0f, player.position.z + 7.0f);
//        offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * offset;
//        transform.position = player.position + offset;
//        transform.LookAt(player.position);
//    }

//    void LateUpdate()
//    {
//        transform.position = player.position + offset;
//        if (Input.GetMouseButton(1))
//        {
//            offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * offset;
//            offset = Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * turnSpeed, Vector3.right) * offset;
//            transform.LookAt(player.position);
//        }
//    }
//}
[AddComponentMenu("Camera-Control/Mouse Orbit with zoom")]
public class CameraFollowing : MonoBehaviour
{

    private Transform player;
    public float distance = 5.0f;
    public float xSpeed = 120.0f;
    public float ySpeed = 120.0f;

    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;

    public float distanceMin = 5f;
    public float distanceMax = 50f;

    private Rigidbody rigidbody;

    float x = 0.0f;
    float y = 0.0f;

    // Use this for initialization
    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        rigidbody = GetComponent<Rigidbody>();

        // Make the rigid body not change rotation
        if (rigidbody != null)
        {
            rigidbody.freezeRotation = true;
        }
        x += Input.GetAxis("Mouse X") * xSpeed * distance * 0.02f;
        y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

        y = ClampAngle(y, yMinLimit, yMaxLimit);

        Quaternion rotation = Quaternion.Euler(y, x, 0);

        distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);

        RaycastHit hit;
        if (Physics.Linecast(player.position, transform.position, out hit))
        {
            distance -= hit.distance;
        }
        Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
        Vector3 position = rotation * negDistance + player.position;

        transform.rotation = rotation;
        transform.position = position;
    }

    void LateUpdate()
    {
        if (player && Input.GetMouseButton(1))
        {
            x += Input.GetAxis("Mouse X") * xSpeed * distance * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

            y = ClampAngle(y, yMinLimit, yMaxLimit);

            Quaternion rotation = Quaternion.Euler(y, x, 0);
            transform.rotation = rotation;
        }

        distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);
        Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
        Vector3 position = transform.rotation * negDistance + player.position;
        transform.position = position;
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }

    public void SetCurrentPlayer(Transform transform)
    {
        player = transform;
    }
}
