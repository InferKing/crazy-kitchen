using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class PlaceToCook : Interactable
{
    [SerializeField] private LayerMask _interactableMask;
    [SerializeField] private HobToggle _toggle;
    [SerializeField] private float _maxDistance = 0.2f;
    private BoxCollider _collider;
    private Dishes _dish;
    public bool IsEmpty { get; private set; } = true;

    protected override void Start()
    {
        base.Start();
        _collider = GetComponent<BoxCollider>();
    }
    protected virtual void Update()
    {
        RaycastHit hit;
        Ray ray = new(transform.position, Vector3.up);
#if UNITY_EDITOR
        Debug.DrawRay(ray.origin, ray.direction * _maxDistance, Color.red);
#endif
        if (Physics.Raycast(ray, out hit, _maxDistance, _interactableMask))
        {
            IsEmpty = false;
#if UNITY_EDITOR
            Debug.DrawRay(ray.origin, ray.direction * _maxDistance, Color.green);
#endif
        }
        else
        {
            IsEmpty = true;
            _collider.enabled = true;
            _dish = null;
        }
    }
    public override bool TryCombine(Interactable interactable, out bool stayInHand)
    {
        stayInHand = false;
        if (interactable == null) return false;
        if (interactable is Dishes && IsEmpty)
        {
            // сковородка забирается сама, а не по указке. это не позволяет проверять, есть ли сейчас сковородка на плите. 
            // решение этому:
            // 1 - каждый раз при интеракции со сковородкой или другим предметом уведомлять об этом всех!!!! (хуита)
            // 2 - переделать плиту так, чтобы она сама проверяла, есть ли сейчас над комфоркой посуда или нет (тоже хуита, 4 комфорки = 4 рэйкаста)
            // 3 - то же, что и вариант 2, но проверять триггерами (хз пока)
            ToStove((Dishes)interactable);
            // GameObject new_mesh = GetGameObject();
            // ServiceLocator.Instance.Get<EventBus>().Invoke(new DestroyMeDaddySignal(interactable.gameObject));
            return true;
        }
        return false;
    }
    public void ToStove(Dishes dish)
    {
        _dish = dish;
        _dish.transform.SetParent(transform);
        _dish.transform.eulerAngles = _dish.InitRotation;
        _dish.Rb.isKinematic = true;
        _dish.transform.position = new Vector3(transform.position.x, transform.position.y + 0.03f, transform.position.z);
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
