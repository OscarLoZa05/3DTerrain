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
    public bool _isDead = false;

    //Inputs
    public Vector2 moveValue;
    InputAction _moveInput;
    InputAction _jumpAction;

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
    private float _sensorRadius = 0.65f;
    

    void Awake()
    {
        _moveInput = InputSystem.actions["Move"];
        _jumpAction = InputSystem.actions["Jump"];
        _controller = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();

        _mainCamera = Camera.main.transform;
    }

    void Update()
    {
        if (_isDead == true)
        {
            return;
        }
        
        moveValue = _moveInput.ReadValue<Vector2>();


        if (_jumpAction.WasPerformedThisFrame() && IsGrounded())
        {
            Jump();
        }

        Movement();

        Gravity();
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

    void Jump()
    {
        _animator.SetBool("IsJumping", true);
        _playerGravity.y = Mathf.Sqrt(_jumpForce * -2 * _gravity);

        _controller.Move(_playerGravity * Time.deltaTime);
    }

    void Gravity()
    {
        if (!IsGrounded())
        {
            _playerGravity.y += _gravity * Time.deltaTime;
        }
        else if (IsGrounded() && _playerGravity.y < 0)
        {
            _animator.SetBool("IsJumping", false);
            _playerGravity.y = -9.81f;
        }
        _controller.Move(_playerGravity * Time.deltaTime);
    }
    
    bool IsGrounded()
    {
        return Physics.CheckSphere(_sensor.position, _sensorRadius, _groundLayer);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(_sensor.position, _sensorRadius);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer == 6)
        {
            Debug.Log("Mierda");
            Death();
        }
    }
    
    void Death()
    {
        _animator.SetTrigger("IsDead");
        _isDead = true;
    }
    
}
