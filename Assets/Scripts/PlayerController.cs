using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    //Componentes
    private Animator _animator;
    private CharacterController _controller;
    
    //Movimiento
    private float _playerSpeed = 6;
    private float _jumpForce = 6;

    //Inputs
    public Vector2 moveValue;
    InputAction _moveInput;

    //Camara
    private float _smoothTime = 0.2f;
    private float _turnSmoothVelocity;
    private Transform _mainCamera;

    //Gravedad
    private float _gravity = -9.81f;
    private Vector3 _playerGravity;
        //Suelo
        public Transform _sensor;
        public LayerMask _groundLayer;
        private float _sensorRadius = 4;
    

    void Awake()
    {
        _moveInput = InputSystem.actions["Move"];
        _controller = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();

        _mainCamera = Camera.main.transform;
    }

    void Update()
    {
        moveValue = _moveInput.ReadValue<Vector2>();

        Movement();
    }

    void Movement()
    {
        Vector3 direction = new Vector3(moveValue.x, 0, moveValue.y);

        _animator.SetFloat("Horizontal", moveValue.x);
        _animator.SetFloat("Vertical", moveValue.y);

        if (direction != Vector3.zero)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _mainCamera.eulerAngles.y;
            float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _smoothTime);
            transform.rotation = Quaternion.Euler(0, smoothAngle, 0);

            Vector3 moveDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            _controller.Move(moveDirection.normalized * _playerSpeed * Time.deltaTime);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(_sensor.position, _sensorRadius);
    }
    
}
