using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("이동 속도")]
    [SerializeField] public float _moveSpeed;
    
    [Header("회전 속도")]
    [SerializeField] private float _rotSpeed;
    
    private Rigidbody _rigidbody;
    Vector3 _movement = Vector3.zero;

    private bool _prevIsMoving;
    public bool IsMoving { get; private set; }

    public event Action<float> OnMove;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    
    private void FixedUpdate()
    {
        if (!GameManager.Instance.IsGameRunning) return;
        
        Rotate();
        Move();
    }

    private void Update()
    {
        if (!GameManager.Instance.IsGameRunning) return;

        HandleMovement();
    }
    
    private void Rotate()
    {
        if (!IsMoving) return;
        
        transform.forward = _movement;
    }

    private void Move()
    {
        if (!IsMoving)
        {
            _rigidbody.linearVelocity = Vector3.zero;
            return;
        }

        _rigidbody.MovePosition(_rigidbody.position + _movement * _moveSpeed * Time.fixedDeltaTime);
    }

    private void HandleMovement()
    {
        _prevIsMoving = IsMoving;
        
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.z = Input.GetAxisRaw("Vertical");
        
        _movement.Normalize();

        IsMoving = _movement != Vector3.zero;

        if (IsMoving != _prevIsMoving)
        {
            OnMove?.Invoke(_movement.magnitude);
        }
    }
}
