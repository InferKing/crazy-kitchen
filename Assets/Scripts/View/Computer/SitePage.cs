using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SitePage : MonoBehaviour
{
    [SerializeField] private GameObject _pageLayout;
    [field: SerializeField] public SitePageType PageType { get; private set; }
    public virtual bool Active
    {
        get
        {
            return _pageLayout.activeSelf;
        }
        set
        {
            if (_pageLayout != null)
            {
                _pageLayout.SetActive(value);
            }
#if UNITY_EDITOR
            else
            {
                Debug.LogWarning($"Page layout must be setted");
            }
#endif
        }
    }
    public virtual void UpdatePage(SiteSession session) { }
}
