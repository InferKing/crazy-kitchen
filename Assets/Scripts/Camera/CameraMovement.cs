using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float _mouseSensitivity, _maxLookAngle;
    private float _yaw, _pitch;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        _yaw = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * _mouseSensitivity;
        _pitch -= _mouseSensitivity * Input.GetAxis("Mouse Y");
        _pitch = Mathf.Clamp(_pitch, -_maxLookAngle, _maxLookAngle);
        transform.localEulerAngles = new Vector3(_pitch, _yaw, 0);
    }
}