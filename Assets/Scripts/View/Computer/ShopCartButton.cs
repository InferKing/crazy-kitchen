using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopCartButton : SiteButton
{
    public override void OnClick()
    {
        _layout.OpenPage(SitePageType.Shop_Cart);
    }
}
