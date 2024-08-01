using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SiteLayout : MonoBehaviour
{
    [SerializeField] private List<SitePage> _pages;
    private void OnEnable()
    {
        IsOpen = true;
    }
    private void OnDisable()
    {
        IsOpen = false;
    }
    public bool IsOpen { get; private set; }
    public SitePage ActivePage { get; private set; }
    public List<SitePage> Pages => _pages.GetRange(0, _pages.Count);
    public SiteSession Session { get; } = new();
    public void OpenPage(SitePageType pageType)
    {
        var page = Pages.FirstOrDefault(item => item.PageType == pageType);

        if (page == null)
        {
#if UNITY_EDITOR
            Debug.LogWarning($"Page with type {pageType} not found.");
#endif
            return;
        }
        ActivePage = page;
        Pages.ForEach(item =>
        {
            if (item.PageType != pageType)
            {
                item.Active = false;
            }
        });
        ActivePage.Active = true;
        ActivePage.UpdatePage(Session);
    }
}
