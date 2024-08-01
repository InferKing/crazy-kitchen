using UnityEngine;

public class CartButton : SiteButton
{
    [SerializeField] private ShopCard _shopCard;
    public override void OnClick()
    {
        _layout.Session.Add(SitePageType.Shop_Cart, _shopCard);
        ServiceLocator.Instance.Get<EventBus>().Invoke(new CartUpdatedSignal(_layout.Session));
    }
}
