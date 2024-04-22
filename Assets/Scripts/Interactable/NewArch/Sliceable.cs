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
        _actionKeys.Add(KeyCode.E, () => Slice());
    }
    public virtual void Slice()
    {
        foreach (var item in ItemsToSpawn)
        {
            GameObject obj = Instantiate(item);
            obj.transform.position = transform.position;
        }
        Destroy(gameObject);
    }
}
