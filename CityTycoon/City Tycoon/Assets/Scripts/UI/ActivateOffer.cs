using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateOffer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        BusinessEvents.current.OnForSaleStep += Activate;
        Deactivate();
    }

    private void Activate()
    {
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        BusinessEvents.current.OnForSaleStep -= Activate;
    }
}
