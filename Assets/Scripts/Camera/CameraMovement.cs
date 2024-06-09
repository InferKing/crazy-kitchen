using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float _mouseSensitivity, _maxLookAngle;
    private float _yaw, _pitch;
    private EventBus _bus;
    private bool _locked = false;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _bus = ServiceLocator.Instance.Get<EventBus>();
        _bus.Subscribe<ToggleRotationSignal>(OnToggleRotation);
    }
    private void Update()
    {
        if (_locked) return;
        _yaw = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * _mouseSensitivity;
        _pitch -= _mouseSensitivity * Input.GetAxis("Mouse Y");
        _pitch = Mathf.Clamp(_pitch, -_maxLookAngle, _maxLookAngle);
        transform.localEulerAngles = new Vector3(_pitch, _yaw, 0);
    }
    private void OnToggleRotation(ToggleRotationSignal signal)
    {
        _locked = signal.data;
    }
    private void OnDisable()
    {
        if (_bus == null) return;
        _bus.Unsubscribe<ToggleRotationSignal>(OnToggleRotation);
    }
}