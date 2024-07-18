public class ExitSiteButton : SiteButton
{
    public override void OnClick()
    {
        _layout.gameObject.SetActive(false);
    }
}
