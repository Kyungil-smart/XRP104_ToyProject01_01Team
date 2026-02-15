using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("이동 속도")]
    [SerializeField] public float _moveSpeed;
    
    [Header("회전 속도")]
    [SerializeField] private float _rotSpeed;

    public bool isMoving => _dir != Vector3.zero;
    
    private Rigidbody _rigidbody;
    Vector3 _dir = Vector3.zero;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Rotate();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Rotate()
    {
        _dir.x = Input.GetAxisRaw("Horizontal");
        _dir.z = Input.GetAxisRaw("Vertical");
        
        if (_dir != Vector3.zero)
        {
            transform.forward = _dir;
        }
    }

    private void Move()
    {
        _rigidbody.MovePosition(_rigidbody.position + _dir * _moveSpeed * Time.fixedDeltaTime);
    }
}
