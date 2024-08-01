using UnityEngine;

[RequireComponent(typeof(ShopCard))]
public class ShopItemButton : SiteButton
{
    private ShopCard _shopCard;
    private void Start()
    {
        _shopCard = GetComponent<ShopCard>();
    }
    public override void OnClick()
    {
        _layout.Session.Add(SitePageType.Shop_Picked, _shopCard.Item);
        _layout.OpenPage(SitePageType.Shop_Picked);
    }
}
