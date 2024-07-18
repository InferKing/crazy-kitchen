using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiteLayout : MonoBehaviour
{
    private void OnEnable()
    {
        IsOpen = true;
    }
    private void OnDisable()
    {
        IsOpen = false; 
    }
    public bool IsOpen { get; private set; }
}
