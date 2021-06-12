using UnityEngine;

public class PowerUps : MonoBehaviour
{

    public GameObject OwnedPowerUp { get; set; }

    public void PlacePowerUp()
    {
        Vector3 eulerAngles = transform.rotation.eulerAngles;
        GameObject go = Instantiate(OwnedPowerUp,
            (transform.position - transform.right * 20),
           Quaternion.Euler(eulerAngles.x, eulerAngles.y - 90, eulerAngles.z),
            transform.parent);
        go.transform.localRotation = go.transform.rotation;
        OwnedPowerUp = null;
    }
}
