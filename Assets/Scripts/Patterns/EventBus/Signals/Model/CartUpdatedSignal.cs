using UnityEngine;

public class CartUpdatedSignal
{
    public readonly SiteSession data;
    public CartUpdatedSignal(SiteSession data)
    {
        this.data = data;
    }
}
