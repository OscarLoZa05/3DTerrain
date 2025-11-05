using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movimiento")]
    public float _playerSpeed = 6;
    public float _jumpForce = 6;

    [Header("Inputs")]
    public Vector2 _moveAction;
    InputAction _moveInput;
    

    void Awake()
    {
        _moveInput = InputSystem.actions["Move"];
    }

    void Update()
    {
        _moveAction = _moveInput.ReadValue<Vector2>();
    }

}
