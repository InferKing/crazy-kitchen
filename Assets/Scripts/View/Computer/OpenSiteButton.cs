public class OpenSiteButton : SiteButton
{
    public override void OnClick()
    {
        _layout.gameObject.SetActive(true);
    }
}
