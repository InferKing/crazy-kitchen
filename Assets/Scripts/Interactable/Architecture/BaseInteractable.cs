using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseInteractable : MonoBehaviour
{
    // ��� _actionKeys �� ����������� �������� Interact, ������ ��� ��� ����� ���������� ItemAction
    // ��� ������ �������� (���� ������� � ����������� ���������� � �� �����������). 
    // ��� ����, ����� ������� ������� E �� �����-������ ��������.
    protected Dictionary<KeyCode, Action> _actionKeys;
    public IReadOnlyDictionary<KeyCode, Action> ActionKeys { get { return _actionKeys; } }
    public abstract void OnEnter();
    public abstract void OnExit();
    public abstract void Interact();
}
