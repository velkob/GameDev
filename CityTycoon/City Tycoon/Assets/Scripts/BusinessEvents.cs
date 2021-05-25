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

    public event Action<int> OnBuyingProperty;

    public void BuyingProperty(int price)
    {
        OnBuyingProperty?.Invoke(price);
    }

    public event Action OnUpgradingProperty;

    public void UpgradingProperty()
    {
        OnUpgradingProperty?.Invoke();
    }
}
