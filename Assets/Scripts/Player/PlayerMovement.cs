using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _walkSpeed = 7f, _jumpForce = 3f;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private CharacterController _characterController;
    private Vector3 _playerVelocity = Vector3.zero;
    private float _horizontalInput, _verticalInput;
    private float _gravity = -9.81f;
    private bool _locked = false;
    private EventBus _bus;
    private void Start()
    {
        _bus = ServiceLocator.Instance.Get<EventBus>();
        _bus.Subscribe<ToggleMovementSignal>(OnToggleMovement);
    }
    private void Update()
    {
        if (_locked) return;
        OldInput();
        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;
        forward.y = 0;
        right.y = 0;
        Vector3 fwdRelative = _verticalInput * forward.normalized;
        Vector3 rightRelative = _horizontalInput * right.normalized;
        rightRelative = Vector3.Normalize((fwdRelative + rightRelative)) * _walkSpeed;
        if (!_characterController.isGrounded)
        {
            _playerVelocity.y += _gravity * Time.deltaTime;
        }
        _characterController.Move((rightRelative + _playerVelocity) * Time.deltaTime);
    }
    private void OldInput()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown(KeyCode.Space) && _characterController.isGrounded)
        {
            // Jump();
        }
    }
    private void Jump()
    {
        _playerVelocity.y = Mathf.Sqrt(-_jumpForce * _gravity);
    }
    private void OnToggleMovement(ToggleMovementSignal signal)
    {
        _locked = signal.data;
    }
    private void OnDisable()
    {
        if (_bus == null) return;
        _bus.Unsubscribe<ToggleMovementSignal>(OnToggleMovement);
    }
}