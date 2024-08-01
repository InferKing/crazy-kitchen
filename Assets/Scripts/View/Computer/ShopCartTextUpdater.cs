using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopCartTextUpdater : MonoBehaviour
{
    [SerializeField] private TMP_Text _cartText;
    private EventBus _bus;
    private void Start()
    {
        _bus = ServiceLocator.Instance.Get<EventBus>();
        _bus.Subscribe<CartUpdatedSignal>(UpdateText);
    }
    private void UpdateText(CartUpdatedSignal signal)
    {
        _cartText.text = signal.data.Get(SitePageType.Shop_Cart).Count.ToString();
    }
}
