using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sliceable : Cookable
{
    [SerializeField] private List<GameObject> _itemsToSpawn;
    public IReadOnlyList<GameObject> ItemsToSpawn { get { return _itemsToSpawn; } }
    protected override void Start()
    {
        base.Start();
    }
    public virtual void ToSlice()
    {
        foreach (var item in ItemsToSpawn)
        {
            GameObject obj = Instantiate(item);
            obj.transform.position = transform.position;
        }
        Destroy(gameObject);
    }
    public override bool TryCombine(Interactable interactable, out bool stayInHand)
    {
        stayInHand = false;
        if (interactable == null) return false;
        return false;
    }
}
