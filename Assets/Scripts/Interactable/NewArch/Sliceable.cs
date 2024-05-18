using DG.Tweening;
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
    public virtual void ToSlice(out Transform tr)
    {
        GameObject obj = null;
        foreach (var item in ItemsToSpawn)
        {
            obj = Instantiate(item);
            obj.transform.position = transform.position;
            var temp = obj.GetComponent<Cookable>();
            temp.OnEnter();
            temp.OnExit();
            temp.TotalCookTime = TotalCookTime;
            temp.UpdateStateWhenRange();
        }
        tr = new GameObject().transform;
        tr.position = transform.position;
        Destroy(gameObject);
        Destroy(tr.gameObject, 3);
    }
}
