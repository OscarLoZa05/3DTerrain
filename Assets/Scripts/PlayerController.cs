using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    //Componentes
    private Animator _animator;
    
    [Header("Movimiento")]
    public float _playerSpeed = 6;
    public float _jumpForce = 6;

    [Header("Inputs")]
    public Vector2 moveValue;
    InputAction _moveInput;

    [Header("Camara")]
    public float _smoothTime = 0.2f;
    [SerializeField] private float _turnSmoothVelocity;
    private Transform _mainCamera;
    
    void Awake()
    {
        _moveInput = InputSystem.actions["Move"];

        _animator = GetComponent<Animator>();

        _mainCamera = _mainCamera.main.transform;
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

        if(direction != moveValue.zero)
        {
            float targetAngle = Mathf.Atan(direction.x, direction.z) * Mathf.Rad2Deg + _mainCamera.eulerAngles.y;
            float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _smoothTime);
            transform.rotation = Quaternion.Euler(0, smoothAngle, 0);
        }
    }

}
