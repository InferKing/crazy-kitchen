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
            // ���������� ���������� ����, � �� �� ������. ��� �� ��������� ���������, ���� �� ������ ���������� �� �����. 
            // ������� �����:
            // 1 - ������ ��� ��� ���������� �� ����������� ��� ������ ��������� ���������� �� ���� ����!!!! (�����)
            // 2 - ���������� ����� ���, ����� ��� ���� ���������, ���� �� ������ ��� ��������� ������ ��� ��� (���� �����, 4 �������� = 4 ��������)
            // 3 - �� ��, ��� � ������� 2, �� ��������� ���������� (�� ����)
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
