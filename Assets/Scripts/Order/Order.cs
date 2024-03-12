using UnityEngine;

[CreateAssetMenu(fileName = "Order_Name", menuName = "Order", order = 52)]
public class Order: ScriptableObject
{
    [field: SerializeField] public string DishNameToView { get; private set; }
    [field: SerializeField] public Dish Dish { get; private set; }
    // ����� �������� ���������� - ������� ����� ��������� ���������.
    // ����� �������� ���������� - � ����� ������� �������� �����.
    // ������� ���� ��������, ����� ���� �������� � �.�.
    // ������ ���������� ������������ ��������, ������������� �� Interactable
}
