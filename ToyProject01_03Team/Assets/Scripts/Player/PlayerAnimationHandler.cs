using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour
{
    private readonly int _moveSpeed = Animator.StringToHash("MoveSpeed");
    private readonly int _isAim = Animator.StringToHash("IsAim");
    private readonly int _isFire = Animator.StringToHash("IsFire");
    private readonly int _isDie = Animator.StringToHash("IsDie");

    private Animator _animator;
    private PlayerMovement _movement;
    private PlayerController _controller;
    private PlayerTarget _target;

    private void Awake() => Init();
    private void OnEnable() => SubscribeEvents();
    private void OnDisable() => UnsubscribeEvents();

    private void Init()
    {
        _animator = GetComponent<Animator>();
        _movement = GetComponent<PlayerMovement>();
        _controller = GetComponent<PlayerController>();
        _target = GetComponentInChildren<PlayerTarget>();
    }

    private void SubscribeEvents()
    {
        _movement.OnMove += OnAnimMove;
        _controller.OnFire += OnAnimFire;
        _target.OnDetectedTargetInRange += OnAnimAim;
        _controller.OnDeath += OnAnimDie;
    }

    private void UnsubscribeEvents()
    {
        _movement.OnMove -= OnAnimMove;
        _controller.OnFire -= OnAnimFire;
        _target.OnDetectedTargetInRange -= OnAnimAim;
        _controller.OnDeath -= OnAnimDie;
    }

    private void OnAnimMove(float movement) => _animator.SetFloat(_moveSpeed, movement);
    private void OnAnimAim(bool isAim) => _animator.SetBool(_isAim, isAim);
    private void OnAnimFire() => _animator.SetTrigger(_isFire);
    private void OnAnimDie() => _animator.SetTrigger(_isDie);
}
