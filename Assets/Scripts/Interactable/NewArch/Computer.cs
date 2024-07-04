using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Computer : Interactable
{
    [SerializeField] private Transform _cameraTransform;
    private Vector3 _lastPosition = Vector3.zero;
    protected override void Start()
    {
        base.Start();
        Bus.Subscribe<ExitComputerSignal>(OnExitComputer);
    }
    public override void Interact()
    {
        _lastPosition = Camera.main.transform.position;
        Camera.main.transform.DOMove(_cameraTransform.position, 0.7f);
        Camera.main.transform.DORotate(_cameraTransform.eulerAngles, 0.7f);
        Bus.Invoke(new ToggleInteractSignal(true));
        Bus.Invoke(new ToggleMovementSignal(true));
        Bus.Invoke(new ToggleRotationSignal(true));
        Cursor.lockState = CursorLockMode.Confined;
        OnExit();
    }
    private void OnExitComputer(ExitComputerSignal signal)
    {
        Bus.Invoke(new ToggleInteractSignal(false));
        Bus.Invoke(new ToggleMovementSignal(false));
        Bus.Invoke(new ToggleRotationSignal(false));
        Camera.main.transform.position = _lastPosition;
        Camera.main.transform.localEulerAngles = Vector3.zero;
        Cursor.lockState = CursorLockMode.Locked;
        _lastPosition = Vector3.zero;
    }
    public override void OnEnter()
    {
        if (_lastPosition != Vector3.zero) return;
        base.OnEnter();
        Bus.Invoke(new ShowItemTextSignal(Constants.keyPressEItem));
    }
    public override void OnExit()
    {
        base.OnExit();
        Bus.Invoke(new ShowItemTextSignal(string.Empty));
    }
    private void OnDisable()
    {
        if (Bus == null) return;
        Bus.Unsubscribe<ExitComputerSignal>(OnExitComputer);
    }
}
