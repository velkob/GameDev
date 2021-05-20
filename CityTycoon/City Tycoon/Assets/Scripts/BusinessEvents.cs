using System;
using UnityEngine;

public class BusinessEvents : MonoBehaviour
{

    public static BusinessEvents current;
    // Start is called before the first frame update
    void Awake()
    {
        current = this;
    }

    public event Action OnForSaleStep;

    public void ForSaleStep()
    {
        OnForSaleStep?.Invoke();
    }

    public event Action OnBuyingProperty;

    public void BuyingProperty()
    {
        OnBuyingProperty?.Invoke();
    }
}
