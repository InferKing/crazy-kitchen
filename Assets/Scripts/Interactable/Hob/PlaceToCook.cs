using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class PlaceToCook : Interactable
{
    [SerializeField] private HobToggle _toggle;
    private BoxCollider _collider;
    private Dishes _dish;
    public bool IsEmpty { get; private set; } = true;

    protected override void Start()
    {
        base.Start();
        _collider = GetComponent<BoxCollider>();
    }
    public override bool TryCombine(Interactable interactable, out bool stayInHand)
    {
        stayInHand = false;
        if (interactable == null) return false;
        if (interactable is Dishes)
        {
            // сковородка забирается сама, а не по указке. это не позволяет проверять, есть ли сейчас сковородка на плите. 
            // решение этому:
            // 1 - каждый раз при интеракции со сковородкой или другим предметом уведомлять об этом всех!!!! (хуита)
            // 2 - переделать плиту так, чтобы она сама проверяла, есть ли сейчас над комфоркой посуда или нет (тоже хуита, 4 комфорки = 4 рэйкаста)
            // 3 - то же, что и вариант 2, но проверять триггерами (хз пока)
            Debug.Log("here");
            ToStove((Dishes)interactable);
            // GameObject new_mesh = GetGameObject();
            // ServiceLocator.Instance.Get<EventBus>().Invoke(new DestroyMeDaddySignal(interactable.gameObject));
            return true;
        }
        return false;
    }
    public void ToStove(Dishes dish)
    {
        if (!IsEmpty) return;
        _dish = dish;
        _dish.transform.SetParent(transform);
        _dish.transform.eulerAngles = _dish.InitRotation;
        _dish.Rb.isKinematic = true;
        _dish.transform.position = transform.position;
        _collider.enabled = false;
    }

    //public void OffStove()
    //{
    //    if (IsEmpty) return;
    //    _dish = null;
    //    _dish.Rb.isKinematic = false;
    //    _collider.enabled = true;
    //}
}
