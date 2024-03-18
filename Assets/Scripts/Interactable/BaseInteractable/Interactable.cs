using System.Collections.Generic;
using UnityEngine;

public class Interactable : BaseInteractable
{
    [SerializeField] private List<GameObject> _gameObjectsToChange;
    [SerializeField] private bool _canGet = true;
    [SerializeField] private Vector3 _initRotation = Vector3.zero;
    [SerializeField] private Vector3 _placeRotation = Vector3.zero;
    public Rigidbody Rb { get; set; }
    private int _counter = 0;
    private GameObject _curGO;
    public bool CanSpawn { get; protected set; } = true;
    public Vector3 InitRotation { get { return _initRotation; } }
    public Vector3 PlaceRoatation { get { return _placeRotation; } }
    public Vector3 IgnoreYRotation { get { return new Vector3(InitRotation.x, transform.rotation.eulerAngles.y, InitRotation.z); } }
    public IReadOnlyList<GameObject> GameObjectsToChange { get { return _gameObjectsToChange; } }
    public bool CanGet { get { return _canGet; } }
    public void UpdateChildRotation(Quaternion rotation)
    {
        _curGO.transform.localRotation = rotation;
    }
    private void Awake()
    {
        _curGO = _gameObjectsToChange[0];
    }
    public override void Combine(Interactable interactable)
    {
        throw new System.NotImplementedException();
    }

    public override void Drop()
    {
        throw new System.NotImplementedException();
    }

    public override void Interact()
    {
        throw new System.NotImplementedException();
    }

    public override void OnEnter()
    {
        if (_curGO.TryGetComponent(out Outline outline))
        {
            outline.enabled = true;
        }
        else
        {
            outline = _curGO.AddComponent<Outline>();
            outline.OutlineWidth = 5;
        }
    }
    public override void OnExit()
    {
        if (_curGO.TryGetComponent(out Outline outline))
        {
            outline.enabled = false;
        }
    }
    public virtual GameObject GetGameObject()
    {
        _counter += 1;
        if (_counter == GameObjectsToChange.Count)
        {
            CanSpawn = false;
        }
        _counter = Mathf.Clamp(_counter, 0, GameObjectsToChange.Count - 1);
        _curGO = _gameObjectsToChange[_counter];
        if (GameObjectsToChange.Count > 1)
        {
            _gameObjectsToChange[_counter - 1].SetActive(false);
            Rb = _gameObjectsToChange[_counter].GetComponent<Rigidbody>();
        }
        _gameObjectsToChange[_counter].SetActive(true);
        if (GameObjectsToChange.Count > 1)
        {
            _gameObjectsToChange[_counter].transform.position = _gameObjectsToChange[_counter - 1].transform.position;
        }
        return _gameObjectsToChange[_counter];
    }
    public override bool TryCombine(Interactable interactable, out bool stayInHand)
    {
        throw new System.NotImplementedException();
    }
}
