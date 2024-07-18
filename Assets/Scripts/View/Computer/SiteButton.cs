using System;
using UnityEngine;

public class SiteButton : MonoBehaviour
{
    [SerializeField] protected SiteLayout _layout;
    public virtual void OnClick()
    {
        throw new NotImplementedException();
    }
}
